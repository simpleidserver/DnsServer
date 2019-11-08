// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class AResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.A;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new AResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.Address = string.Join(".", context.NextBytes(rdataLength).Select(s => s.ToString()));
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            context.WriteUInt16(4);
            context.WriteIPV4(((AResourceRecord)resourceRecord.ResourceRecord).Address);
        }
    }
}
