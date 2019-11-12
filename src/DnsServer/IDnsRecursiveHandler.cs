using DnsServer.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public interface IDnsRecursiveHandler
    {
        Task<DNSResponseMessage> Handle(DNSRequestMessage request, CancellationToken token);
    }
}
