namespace DnsServer.Messages
{
    public class DNSZoneLabel
    {
        public DNSZoneLabel(string label, uint currentOffset)
        {
            Label = label;
            CurrentOffset = currentOffset;
        }

        public string Label { get; set; }
        public uint CurrentOffset { get; set; }
    }
}
