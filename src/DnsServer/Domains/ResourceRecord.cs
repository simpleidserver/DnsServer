// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class ResourceRecord
    {
        public ResourceRecord(int ttl)
        {
            Ttl = ttl;
            SubZoneName = string.Empty;
        }

        public ResourceRecord(int ttl, ResourceTypes resourceType, ResourceClasses resourceClass) : this(ttl)
        {
            ResourceType = resourceType;
            ResourceClass = resourceClass;
        }
        
        public string SubZoneName { get; set; }
        public ResourceTypes ResourceType { get; set; }
        public ResourceClasses ResourceClass { get; set; }
        public int Ttl { get; set; }
    }
}