using DnsServer.Messages;
using DnsServer.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsRequestHandler : IDnsRequestHandler
    {
        private readonly IDnsZoneRepository _dnsZoneRepository;

        public DnsRequestHandler(IDnsZoneRepository dnsZoneRepository)
        {
            _dnsZoneRepository = dnsZoneRepository;
        }

        public async Task<DNSResponseMessage> Handle(DNSRequestMessage dnsRequestMessage, CancellationToken token)
        {
            var result = new DNSResponseMessage
            {
                Header = new DNSHeader
                {
                    Id = dnsRequestMessage.Header.Id,
                    QdCount = dnsRequestMessage.Header.QdCount,
                    AnCount = (uint)dnsRequestMessage.Questions.Count(),
                    Flag = DNSHeaderFlags.RESPONSE
                },
                Questions = dnsRequestMessage.Questions
            };
            var zoneLabels = dnsRequestMessage.Questions.Select(q => q.Label);
            var zones = await _dnsZoneRepository.FindDNSZoneByLabels(zoneLabels, token);
            foreach(var question in dnsRequestMessage.Questions)
            {
                var zone = zones.First(r => r.ZoneLabel == question.Label);
                foreach(var record in zone.ResourceRecords.Where(r => r.ResourceClass.Equals(question.QClass) && r.ResourceType.Equals(question.QType)))
                {
                    result.Answers.Add(new DNSResourceRecord { Name = zone.ZoneLabel, ResourceRecord = record });
                }
            }

            return result;
        }
    }
}
