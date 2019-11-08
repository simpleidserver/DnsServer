// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace DnsServer.Domains
{
    public class DNSZone
    {
        public DNSZone(string zoneLabel)
        {
            ZoneLabel = zoneLabel;
        }

        /// <summary>
        /// Zone label.
        /// </summary>
        public string ZoneLabel { get; set; }
        /// <summary>
        /// Resource records
        /// </summary>
        public ICollection<ResourceRecord> ResourceRecords { get; set; }
    }
}
