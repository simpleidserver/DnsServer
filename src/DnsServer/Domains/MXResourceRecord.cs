// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace DnsServer.Domains
{
    public class MXResourceRecord : ResourceRecord
    {
        public MXResourceRecord(int ttl) : base(ttl, ResourceTypes.MX, ResourceClasses.IN) { }

        public MXResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.MX, resourceClass) { }

        /// <summary>
        /// A 16 bit integer which specifies the preference given to this RR among others at the same owner.  Lower values are preferred.
        /// </summary>
        public UInt16 Preference { get; set; }
        /// <summary>
        /// A <domain-name> which specifies a host willing to act as a mail exchange for the owner name.
        /// </summary>
        public string Exchange { get; set; }
    }
}
