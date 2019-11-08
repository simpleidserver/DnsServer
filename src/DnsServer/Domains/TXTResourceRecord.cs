// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class TXTResourceRecord : ResourceRecord
    {
        public TXTResourceRecord(int ttl) : base(ttl, ResourceTypes.TXT, ResourceClasses.IN)
        {

        }

        public TXTResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.TXT, resourceClass)
        {

        }

        /// <summary>
        /// One or more character strings
        /// </summary>
        public string TxtData { get; set; }
    }
}
