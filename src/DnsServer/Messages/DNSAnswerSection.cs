using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class DNSAnswerSection
    {
        public DNSAnswerSection(string label, ResourceRecord resourceRecord)
        {
            Label = label;
            ResourceRecord = resourceRecord;
        }

        /// <summary>
        /// Domain name.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Resource record.
        /// </summary>
        public ResourceRecord ResourceRecord { get; set; }
    }
}
