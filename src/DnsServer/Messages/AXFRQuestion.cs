// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Messages
{
    public class AXFRQuestion
    {
        /// <summary>
        /// Name of the zone requested
        /// </summary>
        public string Name { get; set; }
    }
}
