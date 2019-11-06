using DnsServer.Domains;
using DnsServer.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public interface IDnsResolver
    {
        Task<DNSResponseMessage> Resolve(string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken cancellationToken);
    }
}
