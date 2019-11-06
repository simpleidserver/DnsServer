namespace DnsServer.Domains
{
    public class MXResourceRecord : ResourceRecord
    {
        public MXResourceRecord() : base(ResourceTypes.MX, ResourceClasses.IN) { }

        public MXResourceRecord(ResourceClasses resourceClass) : base(ResourceTypes.MX, resourceClass) { }

        public short Preference { get; set; }
        public string Exchange { get; set; }
    }
}
