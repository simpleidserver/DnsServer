// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace DnsServer.Messages.Builders
{
    public class DNSRequestMessageBuilder
    {
        public DNSRequestMessageQuestionBuilder New()
        {
            var random = new Random();
            return New((UInt16)random.Next(UInt16.MinValue, UInt16.MaxValue));
        }

        public DNSRequestMessageQuestionBuilder New(UInt16 id)
        {
            var dnsRequestMessage = new DNSRequestMessage
            {
                Header = new DNSHeader
                {
                    Id = id,
                    Flag = DNSHeaderFlags.STANDARD_QUERY
                }
            };
            return new DNSRequestMessageQuestionBuilder(dnsRequestMessage);
        }   
    }
}
