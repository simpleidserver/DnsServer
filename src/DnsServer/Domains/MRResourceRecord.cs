// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class MRResourceRecord : ResourceRecord
    {
        public MRResourceRecord(int ttl) : base(ttl, ResourceTypes.MR, ResourceClasses.IN)
        {

        }

        public MRResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.MR, resourceClass)
        {

        }

        /// <summary>
        /// A <domain-name> which specifies a mailbox which is the proper rename of the specified mailbox.
        /// </summary>
        public string NEWNAME { get; set; }
    }
}
