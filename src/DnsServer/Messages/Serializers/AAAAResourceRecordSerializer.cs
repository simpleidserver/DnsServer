// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;

namespace DnsServer.Messages.Serializers
{
    public class AAAAResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.AAAA;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new AAAAResourceRecord(ttl, string.Empty, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.Address = context.NextIPV6();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            context.WriteUInt16(16);
            context.WriteIPV6(((AAAAResourceRecord)resourceRecord.ResourceRecord).Address);
        }
    }
}
