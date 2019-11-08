// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.Persistence.InMemory
{
    public class InMemoryDnsRootServerRepository : IDnsRootServerRepository
    {
        private readonly List<DNSRootServer> _rootServers;

        public InMemoryDnsRootServerRepository(List<DNSRootServer> rootServers)
        {
            _rootServers = rootServers;
        }

        public Task<IEnumerable<DNSRootServer>> FindAll(CancellationToken token = default(CancellationToken))
        {
            return Task.FromResult((IEnumerable<DNSRootServer>)_rootServers);
        }
    }
}
