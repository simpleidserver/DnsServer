// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class MINFOResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.MINFO;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new MINFOResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextUInt16();
            resourceRecord.RMAILBX = context.NextLabel((UInt16)(rdataLength + context.CurrentOffset));
            resourceRecord.EMAILBX = context.NextLabel((UInt16)(rdataLength + context.CurrentOffset));
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var firstKvp = context.GetLabel(((MINFOResourceRecord)resourceRecord.ResourceRecord).RMAILBX, context.Buffer.Count() + 2);
            var secondKvp = context.GetLabel(((MINFOResourceRecord)resourceRecord.ResourceRecord).EMAILBX, context.Buffer.Count() + firstKvp.Key.Count() + 2);
            context.Buffer.AddRange(((UInt16)(firstKvp.Key.Count() + secondKvp.Key.Count())).ToBytes());
            context.Buffer.AddRange(firstKvp.Key);
            context.Buffer.AddRange(secondKvp.Key);
            context.ZoneLabels.Add(firstKvp.Value);
            context.ZoneLabels.Add(secondKvp.Value);
        }
    }
}
