// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class AAAAResourceRecord : ResourceRecord
    {
        public AAAAResourceRecord(int ttl, string subZoneName = "") : base(ttl, ResourceTypes.AAAA, ResourceClasses.IN)
        {
            SubZoneName = subZoneName;
        }

        public AAAAResourceRecord(int ttl, string subZoneName, ResourceClasses resourceClass) : base(ttl, ResourceTypes.AAAA, resourceClass)
        {
            SubZoneName = subZoneName;
        }

        /// <summary>
        /// IPv6 address.
        /// </summary>
        public string Address { get; set; }
    }
}
