// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class TXTResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.TXT;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new TXTResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.TxtData = context.NextString();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var count = (UInt16)((TXTResourceRecord)resourceRecord.ResourceRecord).TxtData.ToBytes().Count();
            context.WriteUInt16(count);
            context.WriteString(((TXTResourceRecord)resourceRecord.ResourceRecord).TxtData);
        }
    }
}
