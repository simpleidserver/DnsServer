// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnsServer.Messages.Builders
{
    public class DNSResponseMessageBuilder
    {
        public DNSResponseMessage BuildFormatError(DNSRequestMessage requestMessage = null)
        {
            return new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = requestMessage.Header.Id,
                    AnCount = 0,
                    ArCount = 0,
                    NsCount = 0,
                    Flag = new DNSHeaderFlags(DNSHeaderFlags.RESPONSE.Value).SetFlag(DNSHeaderFlags.FORMAT_ERROR),
                    QdCount = requestMessage == null ? (UInt16)0: (UInt16)requestMessage.Questions.Count()
                },
                Questions = requestMessage == null ? new List<DNSQuestionSection>() : requestMessage.Questions
            };
        }

        public DNSResponseMessage BuildServerFailure(DNSRequestMessage requestMessage)
        {
            return new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = requestMessage.Header.Id,
                    AnCount = 0,
                    ArCount = 0,
                    NsCount = 0,
                    Flag = new DNSHeaderFlags(DNSHeaderFlags.RESPONSE.Value).SetFlag(DNSHeaderFlags.SERVER_FAILURE),
                    QdCount = (UInt16)requestMessage.Questions.Count()
                },
                Questions = requestMessage.Questions
            };
        }

        public DNSResponseMessage BuildNameError(DNSRequestMessage requestMessage)
        {
            return new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = requestMessage.Header.Id,
                    AnCount = 0,
                    ArCount = 0,
                    NsCount = 0,
                    Flag = new DNSHeaderFlags(DNSHeaderFlags.RESPONSE.Value).SetFlag(DNSHeaderFlags.NAME_ERROR),
                    QdCount = (UInt16)requestMessage.Questions.Count()
                },
                Questions = requestMessage.Questions
            };
        }

        public DNSResponseMessage BuildNotImplemented(DNSRequestMessage requestMessage)
        {
            return new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = requestMessage.Header.Id,
                    AnCount = 0,
                    ArCount = 0,
                    NsCount = 0,
                    Flag = new DNSHeaderFlags(DNSHeaderFlags.RESPONSE.Value).SetFlag(DNSHeaderFlags.NOT_IMPLEMENTED),
                    QdCount = (UInt16)requestMessage.Questions.Count()
                },
                Questions = requestMessage.Questions
            };
        }

        public DNSResponseMessage BuildRefused(DNSRequestMessage requestMessage)
        {
            return new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = requestMessage.Header.Id,
                    AnCount = 0,
                    ArCount = 0,
                    NsCount = 0,
                    Flag = new DNSHeaderFlags(DNSHeaderFlags.RESPONSE.Value).SetFlag(DNSHeaderFlags.REFUSED),
                    QdCount = (UInt16)requestMessage.Questions.Count()
                },
                Questions = requestMessage.Questions
            };
        }
    }
}
