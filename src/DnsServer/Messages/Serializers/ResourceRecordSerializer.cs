// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System.Collections.Generic;

namespace DnsServer.Messages.Serializers
{
    public class ResourceRecordSerializer
    {
        public static Dictionary<ResourceTypes, IResourceRecordSerializer> MAPPING_RESOURCETYPE_TO_SERIALIZERS = new Dictionary<ResourceTypes, IResourceRecordSerializer>
        {
            { ResourceTypes.A, new AResourceRecordSerializer() },
            { ResourceTypes.NS, new NSResourceRecordSerializer() },
            { ResourceTypes.AAAA, new AAAAResourceRecordSerializer() },
            { ResourceTypes.CNAME, new CNAMEResourceRecordSerializer() },
            { ResourceTypes.HINFO, new HINFOResourceRecordSerializer() },
            { ResourceTypes.MB, new MBResourceRecordSerializer() },
            { ResourceTypes.MG, new MGResourceRecordSerializer() },
            { ResourceTypes.MINFO, new MINFOResourceRecordSerializer() },
            { ResourceTypes.MR, new MRResourceRecordSerializer() },
            { ResourceTypes.MX, new MXResourceRecordSerializer() },
            { ResourceTypes.PTR, new PTRResourceRecordSerializer() },
            { ResourceTypes.SOA, new SOAResourceRecordSerializer() },
            { ResourceTypes.TXT, new TXTResourceRecordSerializer() }
        };

        public static DNSResourceRecord Extract(DNSReadBufferContext context)
        {
            var name = context.NextLabel();
            var resourceType = new ResourceTypes(context.NextUInt16());
            var resourceClass = new ResourceClasses(context.NextUInt16());
            var ttl = context.NextInt();
            return MAPPING_RESOURCETYPE_TO_SERIALIZERS[resourceType].Extract(context, name, resourceClass, ttl);
        }

        public static void Serialize(DNSWriterBufferContext context, DNSResourceRecord dnsResourceRecord)
        {
            context.WriteLabel(dnsResourceRecord.Name);
            context.WriteEnum(dnsResourceRecord.ResourceRecord.ResourceType);
            context.WriteEnum(dnsResourceRecord.ResourceRecord.ResourceClass);
            context.WriteInt(dnsResourceRecord.ResourceRecord.Ttl);
            MAPPING_RESOURCETYPE_TO_SERIALIZERS[dnsResourceRecord.ResourceRecord.ResourceType].Serialize(context, dnsResourceRecord);
        }
    }
}
