namespace DnsServer.Domains
{
    public class ResourceRecord
    {
        public ResourceRecord(ResourceTypes resourceType, ResourceClasses resourceClass)
        {
            ResourceType = resourceType;
            ResourceClass = resourceClass;
        }

        public ResourceTypes ResourceType { get; private set; }
        public ResourceClasses ResourceClass { get; private set; }
        public int? Ttl { get; set; }

        public string Name { get; set; }
    }
}