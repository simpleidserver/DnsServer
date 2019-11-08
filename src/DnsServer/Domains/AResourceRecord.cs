// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class AResourceRecord : ResourceRecord
    {
        public AResourceRecord(int ttl) : base(ttl, ResourceTypes.A, ResourceClasses.IN)
        {

        }

        public AResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.A, resourceClass)
        {

        }

        /// <summary>
        /// A 32 bit Internet address.
        /// </summary>
        public string Address { get; set; }
    }
}
