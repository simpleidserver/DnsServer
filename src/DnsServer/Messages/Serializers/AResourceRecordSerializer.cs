using DnsServer.Domains;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class AResourceRecordSerializer : IResourceRecordSerializer
    {
        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new AResourceRecord(resourceClass)
            {
                Ttl = ttl
            };
            var rdataLength = context.NextUInt();
            resourceRecord.Address = string.Join(".", context.NextBytes(rdataLength).Select(s => s.ToString()));
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            context.WriteUInt(4);
            context.WriteIPV4(((AResourceRecord)resourceRecord.ResourceRecord).Address);
        }
    }
}
