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

        public Task<bool> AddZone(string label, CancellationToken token)
        {
            _dnsZones.Add(new DNSZone(label));
            return Task.FromResult(true);
        }

        public Task<bool> UpdateZone(DNSZone dnsZone)
        {
            _dnsZones.Remove(_dnsZones.First(z => z.ZoneLabel == dnsZone.ZoneLabel));
            _dnsZones.Add(dnsZone);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<DNSZone>> FindAll()
        {
            return Task.FromResult((IEnumerable<DNSZone>)_dnsZones);
        }

        public Task<DNSZone> FindDNSZoneByLabel(string label, CancellationToken token)
        {
            return Task.FromResult(_dnsZones.FirstOrDefault(d => label.EndsWith(d.ZoneLabel)));
        }

        public Task<IEnumerable<DNSZone>> FindDNSZoneByLabels(IEnumerable<string> labels, CancellationToken token)
        {
            return Task.FromResult(_dnsZones.Where(d => labels.Contains(d.ZoneLabel)));
        }
    }
}
