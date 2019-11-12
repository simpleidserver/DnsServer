// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Messages;
using System.Collections.Generic;

namespace DnsServer
{
    public class DnsServerConstants
    {
        public static List<DNSRootServer> DefaultRootServers = new List<DNSRootServer>
        {
            new DNSRootServer("a.root-servers.net", "VeriSign, Inc.", "198.41.0.4", "2001:503:ba3e::2:30"),
            new DNSRootServer("b.root-servers.net", "University of Southern California (ISI)", "199.9.14.201", "2001:500:200::b"),
            new DNSRootServer("c.root-servers.net", "Cogent Communications", "192.33.4.12", "2001:500:2::c")
        };

        public static List<ResourceClasses> DefaultQuestionClasses = new List<ResourceClasses>
        {
            QuestionClasses.CH,
            QuestionClasses.CS,
            QuestionClasses.IN,
            QuestionClasses.HS,
            QuestionClasses.START
        };

        public static List<ResourceTypes> DefaultQuestionTypes = new List<ResourceTypes>
        {
            QuestionTypes.A,
            QuestionTypes.AAAA,
            QuestionTypes.AXFR,
            QuestionTypes.CNAME,
            QuestionTypes.HINFO,
            QuestionTypes.MAILA,
            QuestionTypes.MAILB,
            QuestionTypes.MB,
            QuestionTypes.MG,
            QuestionTypes.MINFO,
            QuestionTypes.MR,
            QuestionTypes.MX,
            QuestionTypes.NS,
            QuestionTypes.PTR,
            QuestionTypes.SOA,
            QuestionTypes.STAR,
            QuestionTypes.TXT
            // QuestionTypes.WKS
        };

        public static Dictionary<string, ResourceTypes> DefaultNameToResourceTypes = new Dictionary<string, ResourceTypes>
        {
            { "A", ResourceTypes.A },
            { "AAAA", ResourceTypes.AAAA },
            { "CNAME", ResourceTypes.CNAME },
            { "MB", ResourceTypes.MB },
            { "MG", ResourceTypes.MG },
            { "MINFO", ResourceTypes.MINFO },
            { "MR", ResourceTypes.MR },
            { "MX", ResourceTypes.MX },
            { "NS", ResourceTypes.NS },
            { "PTR", ResourceTypes.PTR },
            { "SOA", ResourceTypes.SOA },
            { "TXT", ResourceTypes.TXT }
        };

        public static Dictionary<string, ResourceClasses> DefaultNamesToResourceClasses = new Dictionary<string, ResourceClasses>
        {
            { "CH", ResourceClasses.CH },
            { "CS", ResourceClasses.CS },
            { "IN", ResourceClasses.IN },
            { "HS", ResourceClasses.HS },
        };
    }
}
