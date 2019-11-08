// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class PTRResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.PTR;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new PTRResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.PTRDNAME = context.NextLabel((UInt16)(rdataLength + context.CurrentOffset));
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var kvp = context.GetLabel(((PTRResourceRecord)resourceRecord.ResourceRecord).PTRDNAME, context.Buffer.Count() + 2);
            context.Buffer.AddRange(((UInt16)kvp.Key.Count()).ToBytes());
            context.Buffer.AddRange(kvp.Key);
            context.ZoneLabels.Add(kvp.Value);
        }
    }
}
