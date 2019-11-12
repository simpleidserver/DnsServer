// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DnsServer.Acceptance.Tests
{
    public class DnsStartup
    {
        private static DnsStartup _instance;
        private static IDnsServerHost _dnsServerHost;

        private DnsStartup()
        {
            _dnsServerHost = new DnsServerHostBuilder(o =>
                {
                    o.ExcludeForwardRequests.Add(new Regex("^.*example\\.com$"));
                    o.ExcludeForwardRequests.Add(new Regex("^.*example\\.com\\.home$"));
                    o.ExcludeForwardRequests.Add(new Regex("^.*in-addr\\.arpa$"));
                })
                .AddDNSZones(new List<DNSZone>
                {
                    new DNSZone("example.com")
                    {
                        ResourceRecords = new List<ResourceRecord>
                        {
                            new AResourceRecord(119)
                            {
                                Address = "127.0.0.1"
                            },
                            new CNAMEResourceRecord(119)
                            {
                                CNAME = "www.example.net"
                            },
                            new CNAMEResourceRecord(119)
                            {
                                CNAME = "www.example.org"
                            },
                            new CNAMEResourceRecord(119)
                            {
                                CNAME = "www.example.com"
                            },
                            new MBResourceRecord(119)
                            {
                                MADNAME = "mail.com"
                            },
                            new MGResourceRecord(119)
                            {
                                MGMNAME = "group.mail.com"
                            },
                            new MINFOResourceRecord(119)
                            {
                                EMAILBX = "admin@mail.com",
                                RMAILBX = "error@mail.com"
                            },
                            new MRResourceRecord(119)
                            {
                                NEWNAME = "name@mail.com"
                            },
                            new MXResourceRecord(119)
                            {
                                Preference = 4,
                                Exchange = "mail.com"
                            },
                            new NSResourceRecord(119)
                            {
                                NSDName = "example.com"
                            },
                            new PTRResourceRecord(119)
                            {
                                PTRDNAME = "example.com"
                            },
                            new SOAResourceRecord(119)
                            {
                                MName = "example.com",
                                RName = "mail.example.com",
                                Serial = 9999,
                                Refresh = 1000,
                                Retry = 1001,
                                Expire = 1002,
                                Minimum = 10
                            },
                            new TXTResourceRecord(119)
                            {
                                TxtData = "test"
                            }
                        }
                    }
                })
                .Build();
            _dnsServerHost.Run();
        }

        public static DnsStartup GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DnsStartup();
            }

            return _instance;
        }

        public void Start()
        {
        }
    }
}
