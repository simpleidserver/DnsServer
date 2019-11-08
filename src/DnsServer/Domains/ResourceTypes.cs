// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace DnsServer.Domains
{
    public class ResourceTypes : BaseDomainEnum
    {
        public ResourceTypes(UInt16 value) : base(value)
        {
        }

        /// <summary>
        /// A host address.
        /// </summary>
        public static ResourceTypes A = new ResourceTypes(1);
        /// <summary>
        /// An authoritative name server.
        /// </summary>
        public static ResourceTypes NS = new ResourceTypes(2);
        /// <summary>
        /// The canonical name for an alias.
        /// </summary>
        public static ResourceTypes CNAME = new ResourceTypes(5);
        /// <summary>
        /// Marks the start of zone of authority.
        /// </summary>
        public static ResourceTypes SOA = new ResourceTypes(6);
        /// <summary>
        /// A mailbox domain name.
        /// </summary>
        public static ResourceTypes MB = new ResourceTypes(7);
        /// <summary>
        /// A mail group member.
        /// </summary>
        public static ResourceTypes MG = new ResourceTypes(8);
        /// <summary>
        /// A mail rename domain name.
        /// </summary>
        public static ResourceTypes MR = new ResourceTypes(9);
        /// <summary>
        /// A null RR.
        /// </summary>
        public static ResourceTypes NULL = new ResourceTypes(10);
        /// <summary>
        /// A well known service description.
        /// </summary>
        public static ResourceTypes WKS = new ResourceTypes(11);
        /// <summary>
        /// Domain name pointer.
        /// </summary>
        public static ResourceTypes PTR = new ResourceTypes(12);
        /// <summary>
        /// Host information.
        /// </summary>
        public static ResourceTypes HINFO = new ResourceTypes(13);
        /// <summary>
        /// Mailbox or mail list information.
        /// </summary>
        public static ResourceTypes MINFO = new ResourceTypes(14);
        /// <summary>
        /// Mail exchange.
        /// </summary>
        public static ResourceTypes MX = new ResourceTypes(15);
        /// <summary>
        /// Text strings.
        /// </summary>
        public static ResourceTypes TXT = new ResourceTypes(16);
        /// <summary>
        /// Maps a domain name to the IP address (Version 6) 
        /// </summary>
        public static ResourceTypes AAAA = new ResourceTypes(28);
    }
}
