// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class MGResourceRecord : ResourceRecord
    {
        public MGResourceRecord(int ttl) : base(ttl, ResourceTypes.MG, ResourceClasses.IN)
        {

        }

        public MGResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.MG, resourceClass)
        {

        }

        /// <summary>
        /// A <domain-name> which specifies a mailbox which is a member of the mail group specified by the domain name.
        /// </summary>
        public string MGMNAME { get; set; }
    }
}
