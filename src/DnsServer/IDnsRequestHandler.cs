using DnsServer.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public interface IDnsRequestHandler
    {
        Task<DNSResponseMessage> Handle(DNSRequestMessage dnsRequestMessage, CancellationToken token);
    }
}
