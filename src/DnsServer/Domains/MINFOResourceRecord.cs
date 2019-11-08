// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Domains
{
    public class MINFOResourceRecord : ResourceRecord
    {
        public MINFOResourceRecord(int ttl) : base(ttl, ResourceTypes.MINFO, ResourceClasses.IN)
        {

        }

        public MINFOResourceRecord(int ttl, ResourceClasses resourceClass) : base(ttl, ResourceTypes.MINFO, resourceClass)
        {

        }

        /// <summary>
        ///  A <domain-name> which specifies a mailbox which is responsible for the mailing list or mailbox.
        ///  If this domain name names the root, the owner of the MINFO RR is responsible for itself.
        ///  Note that many existing mailing lists use a mailbox X-request for the RMAILBX field of mailing list X, e.g., Msgroup-request for Msgroup.
        ///  This field provides a more general mechanism.
        /// </summary>
        public string RMAILBX { get; set; }
        /// <summary>
        /// A <domain-name> which specifies a mailbox which is to receive error messages related to the mailing list or mailbox specified by the owner of the MINFO RR(similar to the ERRORS-TO: field which has been proposed).
        /// If this domain name names the root, errors should be returned to the sender of the message.
        /// </summary>
        public string EMAILBX { get; set; }
    }
}
