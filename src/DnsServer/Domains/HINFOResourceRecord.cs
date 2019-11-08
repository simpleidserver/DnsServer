// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class HINFOResourceRecord : ResourceRecord
    {
        public HINFOResourceRecord(int ttl) : base(ttl, ResourceTypes.HINFO, ResourceClasses.IN)
        {

        }

        public HINFOResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.HINFO, resourceClass)
        {

        }

        /// <summary>
        /// A <character-string> which specifies the CPU type.
        /// </summary>
        public string CPU { get; set; }
        /// <summary>
        /// A <character-string> which specifies the operating system type.
        /// </summary>
        public string OS { get; set; }
    }
}
