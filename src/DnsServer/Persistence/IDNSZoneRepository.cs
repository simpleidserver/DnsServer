using DnsServer.Domains;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer.Persistence
{
    public interface IDnsZoneRepository
    {
        Task<DNSZone> FindDNSZoneByLabel(string label, CancellationToken token);
        Task<IEnumerable<DNSZone>> FindDNSZoneByLabels(IEnumerable<string> labels, CancellationToken token);
    }
}
