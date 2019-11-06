using DnsServer.Domains;
using DnsServer.Extensions;
using System.Linq;

namespace DnsServer.Messages.Serializers
{
    public class NSResourceRecordSerializer : IResourceRecordSerializer
    {
        public DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl)
        {
            var resourceRecord = new NSResourceRecord(resourceClass)
            {
                Ttl = ttl
            };
            var rdataLength = context.NextShort();
            resourceRecord.NSDName = context.NextLabel();
            return new DNSResourceRecord
            {
                Name = name,
                ResourceRecord = resourceRecord
            };
        }

        public void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord)
        {
            var kvp = context.GetLabel(((NSResourceRecord)resourceRecord.ResourceRecord).NSDName, context.Buffer.Count() + 2);
            context.Buffer.AddRange(((uint)kvp.Key.Count()).ToBytes());
            context.Buffer.AddRange(kvp.Key);
            context.ZoneLabels.Add(kvp.Value);
        }
    }
}
