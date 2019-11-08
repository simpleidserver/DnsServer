// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class DNSQuestionSection
    {
        /// <summary>
        /// Domain name.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// A two octet code which specifies the type of the query.
        /// </summary>
        public ResourceTypes QType { get; set; }
        /// <summary>
        /// A two octet code that specifies the class of the query.
        /// </summary>
        public ResourceClasses QClass { get; set; }

        public static DNSQuestionSection Extract(DNSReadBufferContext context)
        {
            var result = new DNSQuestionSection
            {
                Label = context.NextLabel(),
                QType = new QuestionTypes(context.NextUInt16()),
                QClass = new QuestionClasses(context.NextUInt16())
            };
            return result;
        }

        public void Serialize(DNSWriterBufferContext context)
        {
            context.WriteLabel(Label);
            context.WriteEnum(QType);
            context.WriteEnum(QClass);
        }
    }
}
