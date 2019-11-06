namespace DnsServer.Domains
{
    public class NSResourceRecord : ResourceRecord
    {
        public NSResourceRecord() : base(ResourceTypes.NS, ResourceClasses.IN) { }

        public NSResourceRecord(ResourceClasses resourceClass) : base(ResourceTypes.NS, resourceClass) { }

        /// <summary>
        /// A <domain-name> which specifies a host which should be authoritative for the specified class and domain.
        /// </summary>
        public string NSDName { get; set; }
    }
}
