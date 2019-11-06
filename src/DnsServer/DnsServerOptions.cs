namespace DnsServer
{
    public class DnsServerOptions
    {
        public DnsServerOptions()
        {
            TimeOutInMilliSeconds = 400;
        }

        public int TimeOutInMilliSeconds { get; set; }
    }
}
