namespace DnsServer.Domains
{
    public class AResourceRecord : ResourceRecord
    {
        public AResourceRecord() : base(ResourceTypes.A, ResourceClasses.IN)
        {

        }

        public AResourceRecord(ResourceClasses resourceClass) : base(ResourceTypes.A, resourceClass)
        {

        }

        /// <summary>
        /// A 32 bit Internet address.
        /// </summary>
        public string Address { get; set; }
    }
}
