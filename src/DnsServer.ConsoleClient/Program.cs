// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DnsServer.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var dnsServer = new DnsServerHostBuilder(o =>
                {
                    o.ExcludeForwardRequests.Add(new Regex("^.*example\\.com$"));
                    o.ExcludeForwardRequests.Add(new Regex("^.*in-addr\\.arpa$"));
                })
                .AddDNSZones(new List<DNSZone>
                {
                    new DNSZone("example.com")
                    {
                        ResourceRecords = new List<ResourceRecord>
                        {
                            new AResourceRecord(3600)
                            {
                                Address = "127.0.0.1"
                            },
                            new AResourceRecord(3600, "www")
                            {
                                Address = "127.0.0.1"
                            },
                            new AResourceRecord(3600, "ns1")
                            {
                                Address = "127.0.0.1"
                            },
                            new SOAResourceRecord(3600)
                            {
                                MName = "ns1.example.com",
                                RName = "admin.example.com",
                                Serial = 5,
                                Refresh = 604800,
                                Expire = 2419200,
                                Minimum = 604800,
                                Retry = 86400
                            },
                            new NSResourceRecord(3600)
                            {
                                NSDName = "ns1.example.com"
                            }
                        }
                    },
                    new DNSZone("1.0.0.127.in-addr.arpa")
                    {
                        ResourceRecords = new List<ResourceRecord>
                        {
                            new PTRResourceRecord(100)
                            {
                                PTRDNAME = "localhost"
                            }
                        }
                    }
                })
                .AddDNSRootServers(DnsServerConstants.DefaultRootServers)
                .Build();
            dnsServer.Run();
            dnsServer.DnsRequestReceived += HandleDnsRequestReceived;
            dnsServer.DnsServerStarted += HandleDnsServerStarted;
            dnsServer.DnsResponseSent += HandleDnsResponseSent;
            dnsServer.DnsServerStopped += HandleDnsServerStopped;

            Console.WriteLine("Stop the DNS server");
            Console.ReadLine();
            dnsServer.Stop();

            Console.WriteLine("Press a key to quit the application");
            Console.ReadKey();
        }

        private static void HandleDnsResponseSent(object sender, Events.DnsResponseSentEventArgs e)
        {
            Console.WriteLine($"Response {e.DnsResponseMessage.Header.Id} sent");
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