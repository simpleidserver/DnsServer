// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Messages;
using System;

namespace DnsServer.Events
{
    public class DnsRequestReceivedEventArgs : EventArgs
    {
        public DnsRequestReceivedEventArgs(DNSRequestMessage dnsRequestMessage)
        {
            DnsRequestMessage = dnsRequestMessage;
        }

        public DNSRequestMessage DnsRequestMessage { get; set; }
    }
}
