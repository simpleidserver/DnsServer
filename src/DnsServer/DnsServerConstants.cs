using DnsServer.Domains;
using System.Collections.Generic;

namespace DnsServer
{
    public class DnsServerConstants
    {
        public static List<DNSRootServer> DefaultRootServers = new List<DNSRootServer>
        {
            new DNSRootServer("a.root-servers.net", "VeriSign, Inc."),
            new DNSRootServer("b.root-servers.net", "University of Southern California (ISI)"),
            new DNSRootServer("c.root-servers.net", "Cogent Communications")
        };
    }
}
