using DnsServer.Domains;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.Persistence
{
    public interface IDnsRootServerRepository
    {
        Task<IEnumerable<DNSRootServer>> FindAll(CancellationToken token = default(CancellationToken));
    }
}
