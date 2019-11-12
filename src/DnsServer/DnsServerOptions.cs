// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DnsServer
{
    public class DnsServerOptions
    {
        public DnsServerOptions()
        {
            ExcludeForwardRequests = new List<Regex>();
            TimeOutInMilliSeconds = 400;
            DefaultCpu = "intel";
            DefaultOS = "win";
            DefaultTtl = 17899;
        }
        
        public List<Regex> ExcludeForwardRequests { get; set; }
        public int TimeOutInMilliSeconds { get; set; }
        public string DefaultCpu { get; set; }
        public string DefaultOS { get; set; }
        public int DefaultTtl { get; set; }
    }
}
