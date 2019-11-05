namespace DnsServer.Messages
{
    public class DNSMessage
    {
        public DNSMessage()
        {
            Header = new DNSHeader();
        }

        public DNSHeader Header { get; set; }
    }
}