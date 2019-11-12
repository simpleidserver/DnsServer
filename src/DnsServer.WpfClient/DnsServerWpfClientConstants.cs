// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DnsServer.WpfClient
{
    public static class DnsServerWpfClientConstants
    {
        public static List<DNSZone> DefaultDNSZones = new List<DNSZone>
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
        };

        public static DnsServerOptions Options = new DnsServerOptions
        {
            ExcludeForwardRequests = new List<Regex>
            {
                new Regex("^.*example\\.com$"),
                new Regex("^.*in-addr\\.arpa$")
            }
        };
    }
}
