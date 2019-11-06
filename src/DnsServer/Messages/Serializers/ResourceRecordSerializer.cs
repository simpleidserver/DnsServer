using DnsServer.Domains;
using System.Collections.Generic;

namespace DnsServer.Messages.Serializers
{
    public class ResourceRecordSerializer
    {
        private static Dictionary<ResourceTypes, IResourceRecordSerializer> MAPPING_RESOURCETYPE_TO_SERIALIZERS = new Dictionary<ResourceTypes, IResourceRecordSerializer>
        {
            { ResourceTypes.A, new AResourceRecordSerializer() },
            { ResourceTypes.NS, new NSResourceRecordSerializer() },
            { ResourceTypes.AAAA, new AAAAResourceRecordSerializer() }
        };

        public static DNSResourceRecord Extract(DNSReadBufferContext context)
        {
            var name = context.NextLabel();
            var resourceType = new ResourceTypes(context.NextUInt());
            var resourceClass = new ResourceClasses(context.NextUInt());
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
