// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;

namespace DnsServer.Messages.Builders
{
    public class DNSRequestMessageQuestionBuilder
    {
        private readonly DNSRequestMessage _dnsRequestMessage;

        public DNSRequestMessageQuestionBuilder(DNSRequestMessage dnsRequestMessage)
        {
            _dnsRequestMessage = dnsRequestMessage;
        }

        public DNSRequestMessageQuestionBuilder AddQuestion(string label, ResourceClasses resourceClass, ResourceTypes resourceType)
        {
            _dnsRequestMessage.Questions.Add(new DNSQuestionSection
            {
                Label = label,
                QClass = resourceClass,
                QType = resourceType
            });
            _dnsRequestMessage.Header.QdCount++;
            return this;
        }

        public DNSRequestMessage Build()
        {
            return _dnsRequestMessage;
        }
    }
}
