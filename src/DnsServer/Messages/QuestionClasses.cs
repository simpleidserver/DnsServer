// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using System;

namespace DnsServer.Messages
{
    public class QuestionClasses : ResourceClasses
    {
        /// <summary>
        /// Any classes.
        /// </summary>
        public static ResourceClasses START = new ResourceClasses(255);

        public QuestionClasses(UInt16 value) : base(value) { }
    }
}
