// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Messages.Serializers;
using DnsServer.Persistence;
using DnsServer.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DnsServer
{
    public class DnsServerHostBuilder
    {
        private UdpClient _udpClient;
        private IServiceCollection _serviceCollection;

        public DnsServerHostBuilder(Action<DnsServerOptions> callback = null)
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<IDnsHandler, DnsAuthoritativeHandler>();
            _serviceCollection.AddTransient<IDnsHandler, DnsRecursiveHandler>();
            _serviceCollection.AddTransient<IDnsResolver, DnsResolver>();
            _serviceCollection.AddSingleton<IDnsRootServerRepository>(new InMemoryDnsRootServerRepository(DnsServerConstants.DefaultRootServers));
            _serviceCollection.AddSingleton<IDnsZoneRepository>(new InMemoryDnsZoneRepository(new List<DNSZone>()));
            _serviceCollection.AddDistributedMemoryCache();
            _serviceCollection.Configure<DnsServerOptions>(callback);
        }

        public DnsServerHostBuilder UseAddress(string ipAddr = "127.0.0.1", int port = 53)
        {
            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(ipAddr), port));
            return this;
        }

        public DnsServerHostBuilder UseAddress(IPEndPoint ipEdp)
        {
            _udpClient = new UdpClient(ipEdp);
            return this;
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

        public DnsServerHost Build()
        {
            return new DnsServerHost(_udpClient, _serviceCollection.BuildServiceProvider());
        }
    }
}