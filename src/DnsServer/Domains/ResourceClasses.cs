// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace DnsServer.Domains
{
    public class ResourceClasses : BaseDomainEnum
    {
        public ResourceClasses(UInt16 value) : base(value) { }

        /// <summary>
        /// The internet.
        /// </summary>
        public static ResourceClasses IN = new ResourceClasses(1);
        /// <summary>
        /// The CSNET class.
        /// </summary>
        public static ResourceClasses CS = new ResourceClasses(2);
        /// <summary>
        /// The CHAOS class.
        /// </summary>
        public static ResourceClasses CH = new ResourceClasses(3);
        /// <summary>
        /// Hesiod.
        /// </summary>
        public static ResourceClasses HS = new ResourceClasses(4);
    }
}
