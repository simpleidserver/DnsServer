// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Exceptions;
using DnsServer.Messages;
using DnsServer.Persistence;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsAuthoritativeHandler : IDnsAuthoritativeHandler
    {
        private readonly IDnsZoneRepository _dnsZoneRepository;
        private readonly DnsServerOptions _options;

        public DnsAuthoritativeHandler(IDnsZoneRepository dnsZoneRepository, IOptions<DnsServerOptions> options)
        {
            _dnsZoneRepository = dnsZoneRepository;
            _options = options.Value;
        }

        public async Task<DNSResponseMessage> Handle(DNSRequestMessage request, CancellationToken token)
        {
            var question = request.Questions.First();
            var zoneLabel = question.Label;
            var zone = await _dnsZoneRepository.FindDNSZoneByLabel(zoneLabel, token);
            if (zone == null)
            {
                throw new DNSNameErrorException();
            }

            var result = new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = request.Header.Id,
                    QdCount = request.Header.QdCount,
                    Flag = DNSHeaderFlags.RESPONSE
                },
                Questions = request.Questions
            };
            if (question.QType.Equals(ResourceTypes.HINFO))
            {
                result.Answers.Add(new DNSResourceRecord
                {
                    Name = zone.ZoneLabel,
                    ResourceRecord = new HINFOResourceRecord(_options.DefaultTtl)
                    {
                        Ttl = _options.DefaultTtl,
                        CPU = _options.DefaultCpu,
                        OS = _options.DefaultOS
                    }
                });
            }

            var subZoneName = zoneLabel.Replace(zoneLabel, "").TrimEnd('.');
            foreach (var record in zone.ResourceRecords.Where(r => r.SubZoneName == subZoneName && r.ResourceClass.Equals(question.QClass) && (r.ResourceType.Equals(question.QType) || question.QType.Equals(QuestionTypes.STAR)) && !(question.QType.Equals(QuestionTypes.HINFO))))
            {
                result.Answers.Add(new DNSResourceRecord { Name = GetZoneName(zone.ZoneLabel, record.SubZoneName), ResourceRecord = record });
            }

            foreach (var record in zone.ResourceRecords.Where(r => r.ResourceType == ResourceTypes.NS || r.ResourceType == ResourceTypes.SOA))
            {
                result.AuthoritativeNamespaceServers.Add(new DNSResourceRecord { Name = GetZoneName(zone.ZoneLabel, record.SubZoneName), ResourceRecord = record });
            }

            foreach(var record in zone.ResourceRecords.Where(r => r.ResourceType == ResourceTypes.A || r.ResourceType == ResourceTypes.AAAA))
            {
                result.AdditionalRecords.Add(new DNSResourceRecord { Name = GetZoneName(zone.ZoneLabel, record.SubZoneName), ResourceRecord = record });
            }

            result.Header.AnCount = (UInt16)result.Answers.Count();
            result.Header.NsCount = (UInt16)result.AuthoritativeNamespaceServers.Count();
            result.Header.ArCount = (UInt16)result.AdditionalRecords.Count();
            return result;
        }

        private static string GetZoneName(string zoneLabel, string subZoneLabel)
        {
            if (!string.IsNullOrWhiteSpace(subZoneLabel))
            {
                return $"{subZoneLabel}.{zoneLabel}";
            }

            return zoneLabel;
        }
    }
}
