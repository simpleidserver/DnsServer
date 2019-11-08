// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class MBResourceRecord : ResourceRecord
    {
        public MBResourceRecord(int ttl) : base(ttl, ResourceTypes.MB, ResourceClasses.IN)
        {

        }

        public MBResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.MB, resourceClass)
        {

        }

        /// <summary>
        /// A <domain-name> which specifies a host which has the specified mailbox.
        /// </summary>
        public string MADNAME { get; set; }
    }
}
