namespace DnsServer.Domains
{
    public class AAAAResourceRecord : ResourceRecord
    {
        public AAAAResourceRecord() : base(ResourceTypes.AAAA, ResourceClasses.IN)
        {

        }

        public AAAAResourceRecord(ResourceClasses resourceClass) : base(ResourceTypes.AAAA, resourceClass)
        {

        }

        /// <summary>
        /// IPv6 address.
        /// </summary>
        public string Address { get; set; }
    }
}
