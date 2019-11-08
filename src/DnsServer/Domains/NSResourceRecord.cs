// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class NSResourceRecord : ResourceRecord
    {
        public NSResourceRecord(int ttl) : base(ttl, ResourceTypes.NS, ResourceClasses.IN) { }

        public NSResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.NS, resourceClass) { }

        /// <summary>
        /// A <domain-name> which specifies a host which should be authoritative for the specified class and domain.
        /// </summary>
        public string NSDName { get; set; }
    }
}
