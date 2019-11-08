// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class HINFOResourceRecordSerializer : IResourceRecordSerializer
    {
        public ResourceTypes ResourceType => ResourceTypes.HINFO;

        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new HINFOResourceRecord(ttl, resourceClass);
            var rdataLength = context.NextInt16();
            resourceRecord.CPU = context.NextString();
            resourceRecord.OS = context.NextString();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var hinfo = (HINFOResourceRecord)resourceRecord.ResourceRecord;
            var cpuPayload = hinfo.CPU.ToBytes();
            var osPayload = hinfo.OS.ToBytes();
            var payload = new List<byte>();
            payload.AddRange(cpuPayload);
            payload.AddRange(osPayload);
            context.WriteInt16((Int16)payload.Count());
            context.Buffer.AddRange(payload);
        }
    }
}
