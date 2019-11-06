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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsResolver : IDnsResolver
    {
        private readonly IDnsRootServerRepository _dnsRootServerRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly DnsServerOptions _dnsServerOptions;

        public DnsResolver(IDnsRootServerRepository dnsRootServerRepository, IDistributedCache distributedCache, IOptions<DnsServerOptions> dnsServerOptions)
        {
            _dnsRootServerRepository = dnsRootServerRepository;
            _distributedCache = distributedCache;
            _dnsServerOptions = dnsServerOptions.Value;
        }

        public async Task<DNSResponseMessage> Resolve(string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken token)
        {
            var rootServers = (await _dnsRootServerRepository.FindAll()).Select(r => r.HostName);
            return await Resolve(zoneName, resourceClass, resourceType, token, rootServers, 0);
        }

        public async Task<DNSResponseMessage> Resolve(string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken token, IEnumerable<string> sList, int currentPosition)
        {
            var cacheKey = $"{zoneName}_{resourceClass}_{resourceType}";
            var cacheValue = await _distributedCache.GetAsync(cacheKey, token);
            if (cacheValue != null)
            {
                return DNSResponseMessage.Extract(cacheValue);
            }

            var requestMessage = new DNSRequestMessageBuilder()
                .New()
                .AddQuestion(zoneName, resourceClass, resourceType)
                .Build();
            var rootServer = sList.ElementAt(currentPosition);
            var requestPayload = requestMessage.Serialize();

            var udpClient = new UdpClient();
            var ipAddrs = Dns.GetHostAddresses(rootServer);
            var remoteEndpoint = new IPEndPoint(ipAddrs.First(), 53);
            try
            {
                await udpClient.SendAsync(requestPayload.ToArray(), requestPayload.Count(), remoteEndpoint).WithCancellation(token, _dnsServerOptions.TimeOutInMilliSeconds);
                var udpResult = await udpClient.ReceiveAsync().WithCancellation(token, _dnsServerOptions.TimeOutInMilliSeconds);
                var responseMessage = DNSResponseMessage.Extract(udpResult.Buffer);
                foreach(var answer in responseMessage.Answers)
                {
                    var ck = $"{answer.Name}_{answer.ResourceRecord.ResourceClass}_{answer.ResourceRecord.ResourceType}";
                    await _distributedCache.SetAsync(ck, responseMessage.Serialize().ToArray(), token);
                }

                if (responseMessage.Answers.Any(a => a.ResourceRecord.ResourceClass.Equals(resourceClass) && a.ResourceRecord.ResourceType.Equals(resourceType)))
                {
                    return responseMessage;
                }

                sList = responseMessage.AuthoritativeNamespaceServers.Select(a => ((NSResourceRecord)a.ResourceRecord).NSDName);
                currentPosition = 0;
                return await Resolve(zoneName, resourceClass, resourceType, token, sList, currentPosition);
            }
            catch(TimeoutException)
            {
                currentPosition++;
                return await Resolve(zoneName, resourceClass, resourceType, token, sList, currentPosition);
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}