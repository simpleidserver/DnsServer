// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Persistence;
using DnsServer.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace DnsServer
{
    public class DnsServerHostBuilder
    {
        private IServiceCollection _serviceCollection;

        public DnsServerHostBuilder(Action<DnsServerOptions> callback = null)
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<IDnsServerHost, DnsServerHost>();
            _serviceCollection.AddTransient<IDnsAuthoritativeHandler, DnsAuthoritativeHandler>();
            _serviceCollection.AddTransient<IDnsRecursiveHandler, DnsRecursiveHandler>();
            _serviceCollection.AddTransient<IDnsResolver, DnsResolver>();
            _serviceCollection.AddSingleton<IDnsRootServerRepository>(new InMemoryDnsRootServerRepository(DnsServerConstants.DefaultRootServers));
            _serviceCollection.AddSingleton<IDnsZoneRepository>(new InMemoryDnsZoneRepository(new List<DNSZone>()));
            _serviceCollection.AddDistributedMemoryCache();
            _serviceCollection.Configure<DnsServerOptions>(callback);
        }

        public DnsServerHostBuilder AddDNSZones(List<DNSZone> dnsZones)
        {
            _serviceCollection.RemoveAll<IDnsZoneRepository>();
            _serviceCollection.AddSingleton<IDnsZoneRepository>(new InMemoryDnsZoneRepository(dnsZones));
            return this;
        }

        public DnsServerHostBuilder AddDNSRootServers(List<DNSRootServer> dnsRootServers)
        {
            _serviceCollection.RemoveAll<IDnsRootServerRepository>();
            _serviceCollection.AddSingleton<IDnsRootServerRepository>(new InMemoryDnsRootServerRepository(dnsRootServers));
            return this;
        }

        public IDnsServerHost Build()
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            return serviceProvider.GetService<IDnsServerHost>();
        }
    }
}