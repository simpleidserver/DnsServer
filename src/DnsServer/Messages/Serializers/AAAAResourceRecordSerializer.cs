using DnsServer.Domains;

namespace DnsServer.Messages.Serializers
{
    public class AAAAResourceRecordSerializer : IResourceRecordSerializer
    {
        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new AAAAResourceRecord(resourceClass)
            {
                Ttl = ttl
            };
            var rdataLength = context.NextShort();
            resourceRecord.Address = context.NextIPV6();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            context.WriteUInt(16);
            context.WriteIPV6(((AAAAResourceRecord)resourceRecord.ResourceRecord).Address);
        }
    }
}
