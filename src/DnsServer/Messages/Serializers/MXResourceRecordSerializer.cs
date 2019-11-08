// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class MXResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.MX;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new MXResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.Preference = context.NextUInt16();
            resourceRecord.Exchange = context.NextLabel((UInt16)(rdataLength + context.CurrentOffset));
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var kvp = context.GetLabel(((MXResourceRecord)resourceRecord.ResourceRecord).Exchange, context.Buffer.Count() + 4);
            context.Buffer.AddRange(((UInt16)(kvp.Key.Count() + 2)).ToBytes());
            context.Buffer.AddRange(((MXResourceRecord)resourceRecord.ResourceRecord).Preference.ToBytes());
            context.Buffer.AddRange(kvp.Key);
            context.ZoneLabels.Add(kvp.Value);
        }
    }
}
