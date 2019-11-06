using DnsServer.Domains;

namespace DnsServer.Messages.Serializers
{
    public interface IResourceRecordSerializer
    {
        DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl);
        void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord);
    }
}
