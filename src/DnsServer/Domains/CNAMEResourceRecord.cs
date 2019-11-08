// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class CNAMEResourceRecord : ResourceRecord
    {
        public CNAMEResourceRecord(int ttl) : base(ttl, ResourceTypes.CNAME, ResourceClasses.IN) { }

        public CNAMEResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.CNAME, resourceClass) { }

        /// <summary>
        ///  A <domain-name> which specifies the canonical or primary name for the owner.The owner name is an alias.
        /// </summary>
        public string CNAME { get; set; }
    }
}
