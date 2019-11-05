using DnsServer.Messages;
using System;

namespace DnsServer.Events
{
    public class DnsRequestReceivedEventArgs : EventArgs
    {
        public DnsRequestReceivedEventArgs(DNSRequestMessage dnsRequestMessage)
        {
            DnsRequestMessage = dnsRequestMessage;
        }

        public DNSRequestMessage DnsRequestMessage { get; set; }
    }
}
