// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using DnsServer.Messages;
using DnsServer.Messages.Builders;
using DnsServer.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsRecursiveHandler : IDnsHandler
    {
        private readonly IDnsRootServerRepository _dnsRootServerRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly DnsServerOptions _dnsServerOptions;

        public DnsRecursiveHandler(IDnsRootServerRepository dnsRootServerRepository, IDistributedCache distributedCache, IOptions<DnsServerOptions> dnsServerOptions)
        {
            _dnsRootServerRepository = dnsRootServerRepository;
            _distributedCache = distributedCache;
            _dnsServerOptions = dnsServerOptions.Value;
        }

        public DnsHandlerTypes Type => DnsHandlerTypes.Recursive;

        public Task<DNSResponseMessage> Handle(DNSRequestMessage request, CancellationToken token)
        {
            var question = request.Questions.First();
            return Resolve(request.Header.Id, question.Label, question.QClass, question.QType, token);
        }

        private async Task<DNSResponseMessage> Resolve(UInt16 requestId, string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken token)
        {
            var rootServers = await _dnsRootServerRepository.FindAll();
            return await Resolve(requestId, zoneName, resourceClass, resourceType, token, rootServers.ToList(), 0);
        }

        private async Task<DNSResponseMessage> Resolve(UInt16 requestId, string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken token, List<DNSRootServer> sList, int currentPosition)
        {
            var cacheKey = $"{zoneName}_{resourceClass}_{resourceType}";
            var cacheValue = await _distributedCache.GetAsync(cacheKey, token);
            if (cacheValue != null)
            {
                var cacheResponse = DNSResponseMessage.Extract(cacheValue);
                cacheResponse.Header.Id = requestId;
                cacheResponse.Header.Flag = DNSHeaderFlags.RESPONSE;
                return cacheResponse;
            }

            var requestMessage = new DNSRequestMessageBuilder()
                .New(requestId)
                .AddQuestion(zoneName, resourceClass, resourceType)
                .Build();
            var rootServer = sList.ElementAt(currentPosition);
            var requestPayload = requestMessage.Serialize();
            var udpClient = new UdpClient();
            IPAddress ipAddr = IPAddress.Parse(rootServer.IPV4Address);
            var remoteEndpoint = new IPEndPoint(ipAddr, 53);
            try
            {
                await udpClient.SendAsync(requestPayload.ToArray(), requestPayload.Count(), remoteEndpoint).WithCancellation(token, _dnsServerOptions.TimeOutInMilliSeconds);
                var udpResult = await udpClient.ReceiveAsync().WithCancellation(token, _dnsServerOptions.TimeOutInMilliSeconds);
                var responseMessage = DNSResponseMessage.Extract(udpResult.Buffer);
                responseMessage.Header.Id = requestId;
                responseMessage.Header.Flag = DNSHeaderFlags.RESPONSE;
                if (responseMessage.Answers.Any(a => a.ResourceRecord.ResourceClass.Equals(resourceClass) && a.ResourceRecord.ResourceType.Equals(resourceType)))
                {
                    // TODO : The caching logic must be changed !
                    var ttl = responseMessage.Answers.First().ResourceRecord.Ttl;
                    await _distributedCache.SetAsync(cacheKey, responseMessage.Serialize().ToArray(), new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMilliseconds(ttl) }, token);
                    return responseMessage;
                }

                sList = new List<DNSRootServer>();
                foreach(var record in responseMessage.AuthoritativeNamespaceServers.Where(a => a.ResourceRecord is NSResourceRecord))
                {
                    var authoritativeNamespaceServer = (NSResourceRecord)record.ResourceRecord;
                    var ipv4Record = responseMessage.AdditionalRecords.FirstOrDefault(a => a.ResourceRecord is AResourceRecord && a.Name == authoritativeNamespaceServer.NSDName);
                    var ipv6Record = responseMessage.AdditionalRecords.FirstOrDefault(a => a.ResourceRecord is AAAAResourceRecord && a.Name == authoritativeNamespaceServer.NSDName);
                    if (ipv4Record == null)
                    {
                        continue;
                    }

                    var ipv6 = ipv6Record == null ? string.Empty : ((AAAAResourceRecord)ipv6Record.ResourceRecord).Address;
                    sList.Add(new DNSRootServer(record.Name, null, ((AResourceRecord)ipv4Record.ResourceRecord).Address, ipv6));
                }

                if (!sList.Any())
                {
                    return responseMessage;
                }

                currentPosition = 0;
                return await Resolve(requestId, zoneName, resourceClass, resourceType, token, sList, currentPosition);
            }
            catch (TimeoutException)
            {
                currentPosition++;
                return await Resolve(requestId, zoneName, resourceClass, resourceType, token, sList, currentPosition);
            }
        }
    }
}
