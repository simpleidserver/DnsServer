using DnsServer.Domains;
using DnsServer.Persistence;
using DnsServer.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DnsServer
{
    public class DnsServerHostBuilder
    {
        private UdpClient _udpClient;
        private IServiceCollection _serviceCollection;

        public DnsServerHostBuilder()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<IDnsRequestHandler, DnsRequestHandler>();
            _serviceCollection.AddSingleton<IDNSZoneRepository>(new InMemoryDNSZoneRepository(new List<DNSZone>()));
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
            _serviceCollection.RemoveAll<IDNSZoneRepository>();
            _serviceCollection.AddSingleton<IDNSZoneRepository>(new InMemoryDNSZoneRepository(dnsZones));
            return this;
        }

        public DnsServerHost Build()
        {
            return new DnsServerHost(_udpClient, _serviceCollection.BuildServiceProvider());
        }
    }
}