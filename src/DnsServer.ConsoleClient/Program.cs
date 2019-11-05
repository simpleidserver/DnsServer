using DnsServer.Domains;
using System;
using System.Collections.Generic;

namespace DnsServer.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = 12 | 0xc000;

            var dnsServer = new DnsServerHostBuilder()
                .UseAddress("127.0.0.1", 53)
                .AddDNSZones(new List<DNSZone>
                {
                    new DNSZone("google.com")
                    {
                        ResourceRecords = new List<ResourceRecord>
                        {
                            new AResourceRecord
                            {
                                Address = "127.0.0.1",
                                Ttl = 119
                            }
                        }
                    }
                })
                .Build();
            dnsServer.Run();
            dnsServer.DnsRequestReceived += HandleDnsRequestReceived;
            dnsServer.DnsServerStarted += HandleDnsServerStarted;
            dnsServer.DnsServerStopped += HandleDnsServerStopped;

            Console.WriteLine("Stop the DNS server");
            Console.ReadLine();
            dnsServer.Stop();

            Console.WriteLine("Press a key to quit the application");
            Console.ReadKey();
        }

        private static void HandleDnsRequestReceived(object sender, Events.DnsRequestReceivedEventArgs e)
        {
            Console.WriteLine($"Request {e.DnsRequestMessage.Header.Id} received");
        }

        private static void HandleDnsServerStopped(object sender, EventArgs e)
        {
            Console.WriteLine("Dns server stopped");
        }

        private static void HandleDnsServerStarted(object sender, EventArgs e)
        {
            Console.WriteLine("Dns server started");
        }
    }
}