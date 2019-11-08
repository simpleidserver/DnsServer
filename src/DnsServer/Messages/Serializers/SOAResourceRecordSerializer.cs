// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class SOAResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.SOA;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new SOAResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            var maxOffset = (UInt16)(rdataLength + context.CurrentOffset);
            resourceRecord.MName = context.NextLabel(maxOffset);
            resourceRecord.RName = context.NextLabel(maxOffset);
            resourceRecord.Serial = context.NextUInt();
            resourceRecord.Refresh = context.NextInt();
            resourceRecord.Retry = context.NextInt();
            resourceRecord.Expire = context.NextInt();
            resourceRecord.Minimum = context.NextUInt();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var firstKvp = context.GetLabel(((SOAResourceRecord)resourceRecord.ResourceRecord).MName, context.Buffer.Count() + 2);
            var secondKvp = context.GetLabel(((SOAResourceRecord)resourceRecord.ResourceRecord).RName, context.Buffer.Count() + firstKvp.Key.Count() + 2);
            context.Buffer.AddRange(((UInt16)(firstKvp.Key.Count() + secondKvp.Key.Count() + (4 * 5) )).ToBytes());
            context.Buffer.AddRange(firstKvp.Key);
            context.Buffer.AddRange(secondKvp.Key);
            context.Buffer.AddRange(((SOAResourceRecord)resourceRecord.ResourceRecord).Serial.ToBytes());
            context.Buffer.AddRange(((SOAResourceRecord)resourceRecord.ResourceRecord).Refresh.ToBytes());
            context.Buffer.AddRange(((SOAResourceRecord)resourceRecord.ResourceRecord).Retry.ToBytes());
            context.Buffer.AddRange(((SOAResourceRecord)resourceRecord.ResourceRecord).Expire.ToBytes());
            context.Buffer.AddRange(((SOAResourceRecord)resourceRecord.ResourceRecord).Minimum.ToBytes());
            context.ZoneLabels.Add(firstKvp.Value);
            context.ZoneLabels.Add(secondKvp.Value);
        }
    }
}
