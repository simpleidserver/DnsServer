namespace DnsServer.Domains
{
    public class DNSRootServer
    {
        public DNSRootServer(string hostName, string manager)
        {
            HostName = hostName;
            Manager = manager;
        }

        public string HostName { get; set; }
        public string Manager { get; set; }
    }
}
