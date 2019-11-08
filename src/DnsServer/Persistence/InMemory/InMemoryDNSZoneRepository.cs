// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.Persistence.InMemory
{
    public class InMemoryDnsZoneRepository : IDnsZoneRepository
    {
        private readonly List<DNSZone> _dnsZones;

        public InMemoryDnsZoneRepository(List<DNSZone> dnsZones)
        {
            _dnsZones = dnsZones;
        }

        public Task<DNSZone> FindDNSZoneByLabel(string label, CancellationToken token)
        {
            return Task.FromResult(_dnsZones.FirstOrDefault(d => d.ZoneLabel == label));
        }

        public Task<IEnumerable<DNSZone>> FindDNSZoneByLabels(IEnumerable<string> labels, CancellationToken token)
        {
            return Task.FromResult(_dnsZones.Where(d => labels.Contains(d.ZoneLabel)));
        }
    }
}
