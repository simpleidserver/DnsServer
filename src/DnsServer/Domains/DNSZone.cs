using System.Collections.Generic;

namespace DnsServer.Domains
{
    public class DNSZone
    {
        public DNSZone(string zoneLabel)
        {
            ZoneLabel = zoneLabel;
        }

        /// <summary>
        /// Zone label.
        /// </summary>
        public string ZoneLabel { get; set; }
        /// <summary>
        /// Resource records
        /// </summary>
        public ICollection<ResourceRecord> ResourceRecords { get; set; }
    }
}
