// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace DnsServer.Messages
{
    public class DNSZoneLabel
    {
        public DNSZoneLabel(string label, uint currentOffset)
        {
            Label = label;
            CurrentOffset = currentOffset;
        }

        public string Label { get; set; }
        public uint CurrentOffset { get; set; }
    }
}
