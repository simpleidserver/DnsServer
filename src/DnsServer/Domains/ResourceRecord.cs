namespace DnsServer.Domains
{
    public class ResourceRecord
    {
        public ResourceRecord()
        {
            Ttl = 559;
        }

        public ResourceRecord(ResourceTypes resourceType, ResourceClasses resourceClass)
        {
            ResourceType = resourceType;
            ResourceClass = resourceClass;
        }
        
        public ResourceTypes ResourceType { get; set; }
        public ResourceClasses ResourceClass { get; set; }
        public int Ttl { get; set; }
    }
}