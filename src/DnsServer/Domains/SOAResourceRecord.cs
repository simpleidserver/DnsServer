// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class SOAResourceRecord : ResourceRecord
    {
        public SOAResourceRecord(int ttl) : base(ttl, ResourceTypes.SOA, ResourceClasses.IN)
        {

        }

        public SOAResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.SOA, resourceClass)
        {

        }

        /// <summary>
        ///The <domain-name> of the name server that was the original or primary source of data for this zone.
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// A <domain-name> which specifies the mailbox of the person responsible for this zone.
        /// </summary>
        public string RName { get; set; }
        /// <summary>
        /// The unsigned 32 bit version number of the original copy of the zone.Zone transfers preserve this value.This value wraps and should be compared using sequence space arithmetic.
        /// </summary>
        public uint Serial { get; set; }
        /// <summary>
        /// A 32 bit time interval before the zone should be refreshed.
        /// </summary>
        public int Refresh { get; set; }
        /// <summary>
        ///  A 32 bit time interval that should elapse before a failed refresh should be retried.
        /// </summary>
        public int Retry { get; set; }
        /// <summary>
        ///  A 32 bit time value that specifies the upper limit on the time interval that can elapse before the zone is no longer authoritative.
        /// </summary>
        public int Expire { get; set; }
        /// <summary>
        /// The unsigned 32 bit minimum TTL field that should be exported with any RR from this zone.
        /// </summary>
        public uint Minimum { get; set; }
    }
}
