// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class DNSRootServer
    {
        public DNSRootServer(string hostName)
        {
            HostName = hostName;
        }

        public DNSRootServer(string hostName, string manager, string ipv4Address, string ipv6Address) : this(hostName)
        {
            Manager = manager;
            IPV4Address = ipv4Address;
            IPV6Address = ipv6Address;
        }

        public string HostName { get; set; }
        public string Manager { get; set; }
        public string IPV4Address { get; set; }
        public string IPV6Address { get; set; }
    }
}
