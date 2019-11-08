// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Messages
{
    public class DNSMessage
    {
        public DNSMessage()
        {
            Header = new DNSHeader();
        }

        public DNSHeader Header { get; set; }
    }
}