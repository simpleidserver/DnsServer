using DnsServer.Domains;
using DnsServer.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.ConsoleClient
{
    class Program
    {
        private static async Task CheckDnsResolver()
        {
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
                // .AddDNSRootServers(new List<DNSRootServer>
                // {
                //     new DNSRootServer("8.8.8.8", "Google")
                // })
                .Build();
            var dnsResolver = (IDnsResolver)dnsServer.ServiceProvider.GetService(typeof(IDnsResolver));
            var firstResult = await dnsResolver.Resolve("google.com", QuestionClasses.IN, QuestionTypes.A, CancellationToken.None);
            var secondResult = await dnsResolver.Resolve("google.com", QuestionClasses.IN, QuestionTypes.A, CancellationToken.None);
        }

        static void Main(string[] args)
        {
            CheckDnsResolver().Wait();
            Console.WriteLine("Press any key to quit the application");
            Console.ReadKey();

            // var dnsServer = new DnsServerHostBuilder()
            //     .UseAddress("127.0.0.1", 53)
            //     .AddDNSZones(new List<DNSZone>
            //     {
            //         new DNSZone("google.com")
            //         {
            //             ResourceRecords = new List<ResourceRecord>
            //             {
            //                 new AResourceRecord
            //                 {
            //                     Address = "127.0.0.1",
            //                     Ttl = 119
            //                 }
            //             }
            //         }
            //     })
            //     .Build();
            // dnsServer.Run();
            // dnsServer.DnsRequestReceived += HandleDnsRequestReceived;
            // dnsServer.DnsServerStarted += HandleDnsServerStarted;
            // dnsServer.DnsServerStopped += HandleDnsServerStopped;
            // 
            // Console.WriteLine("Stop the DNS server");
            // Console.ReadLine();
            // dnsServer.Stop();
            // 
            // Console.WriteLine("Press a key to quit the application");
            // Console.ReadKey();
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