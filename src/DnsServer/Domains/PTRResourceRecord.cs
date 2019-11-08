// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class PTRResourceRecord : ResourceRecord
    {
        public PTRResourceRecord(int ttl) : base(ttl, ResourceTypes.PTR, ResourceClasses.IN)
        {

        }

        public PTRResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.PTR, resourceClass)
        {

        }

        /// <summary>
        /// A <domain-name> which points to some location in the domain name space.
        /// </summary>
        public string PTRDNAME { get; set; }
    }
}
