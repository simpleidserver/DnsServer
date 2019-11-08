// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class DNSResourceRecord
    {
        public string Name { get; set; }
        public ResourceRecord ResourceRecord { get; set; }
    }
}
