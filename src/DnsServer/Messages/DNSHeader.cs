// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace DnsServer.Messages
{
    public class DNSHeader
    {
        /// <summary>
        /// 16 bit identifier assigned by the program that generates any kind of query.
        /// </summary>
        public UInt16 Id { get; set; }
        /// <summary>
        /// Flags
        /// </summary>
        public DNSHeaderFlags Flag { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of entries in the question section.
        /// </summary>
        public UInt16 QdCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the answer section.
        /// </summary>
        public UInt16 AnCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of name server resource records in the authority records section.
        /// </summary>
        public UInt16 NsCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the additional records section.
        /// </summary>
        public UInt16 ArCount { get; set; }
        /// <summary>
        /// Get the length.
        /// </summary>
        public short Length { get => 12; }

        public static DNSHeader Extract(DNSReadBufferContext context)
        {
            var result = new DNSHeader
            {
                Id = context.NextUInt16(),
                Flag = DNSHeaderFlags.Extract(context),
                QdCount = context.NextUInt16(),
                AnCount = context.NextUInt16(),
                NsCount = context.NextUInt16(),
                ArCount = context.NextUInt16()
            };
            return result;
        }

        public void Serialize(DNSWriterBufferContext context)
        {
            context.WriteUInt16(Id);
            context.WriteFlag(Flag);
            context.WriteUInt16(QdCount);
            context.WriteUInt16(AnCount);
            context.WriteUInt16(NsCount);
            context.WriteUInt16(ArCount);
        }
    }
}
