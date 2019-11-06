using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class DNSResourceRecord
    {
        public string Name { get; set; }
        public ResourceRecord ResourceRecord { get; set; }
    }
}
