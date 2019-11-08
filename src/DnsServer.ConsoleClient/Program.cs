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
                    o.ExcludeForwardRequests.Add(new Regex("^.*example\\.com\\.home$"));
                    o.ExcludeForwardRequests.Add(new Regex("^.*in-addr\\.arpa$"));
                })
                .UseAddress("127.0.0.1", 53)
                .AddDNSZones(new List<DNSZone>
                {
                    new DNSZone("example.com")
                    {
                        ResourceRecords = new List<ResourceRecord>
                        {
                            new AResourceRecord(100)
                            {
                                Address = "127.0.0.1"
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