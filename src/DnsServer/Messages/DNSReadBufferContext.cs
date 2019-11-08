// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnsServer.Messages
{
    public class DNSReadBufferContext
    {
        public DNSReadBufferContext(byte[] buffer)
        {
            Buffer = new Queue<byte>(buffer);
            ZoneLabels = new List<ICollection<DNSZoneLabel>>();
            CurrentOffset = 0;
        }

        public Queue<byte> Buffer { get; set; }
        public List<ICollection<DNSZoneLabel>> ZoneLabels { get; set; }
        public UInt16 CurrentOffset { get; private set; }

        public IEnumerable<byte> NextBytes(UInt16 length)
        {
            var result = Buffer.Dequeue(length);
            CurrentOffset += length;
            return result;
        }

        public string NextIPV6()
        {
            var payload = Buffer.Dequeue(16).ToArray();
            var str = new StringBuilder();
            for (var i = 0; i < payload.Count(); i += 2)
            {
                var segment = (ushort)payload[i] << 8 | payload[i + 1];
                str.AppendFormat("{0:X}", segment);
                if (i + 2 != payload.Length)
                {
                    str.Append(':');
                }
            }

            CurrentOffset += 16;
            return str.ToString();
        }

        public int NextInt()
        {
            var result = Buffer.GetInt();
            CurrentOffset += 4;
            return result;
        }

        public UInt16 NextUInt16()
        {
            var result = Buffer.GetUInt16();
            CurrentOffset += 2;
            return result;
        }

        public string NextString()
        {
            var size = Buffer.First();
            var result = Buffer.GetString();
            CurrentOffset += size;
            return result;
        }

        public uint NextUInt()
        {
            var result = Buffer.GetUInt();
            CurrentOffset += 4;
            return result;
        }

        public Int16 NextInt16()
        {
            var result = Buffer.GetInt16();
            CurrentOffset += 2;
            return result;
        }

        public string NextLabel(UInt16? maxOffset = null)
        {
            List<DNSZoneLabel> labels = new List<DNSZoneLabel>();
            InternalNextLabel(labels, maxOffset);
            ZoneLabels.Add(labels);
            return string.Join(".", labels.Select(l => l.Label));
        }

        private void InternalNextLabel(List<DNSZoneLabel> labels, UInt16? maxOffset = null)
        {
            var offset = Buffer.GetUInt16(false);
            if (offset >= 0xc000)
            {
                offset = (UInt16)(NextUInt16() ^ 0xc000);
                var zoneLabel = ZoneLabels.First(m => m.Any(z => z.CurrentOffset == offset));
                int index = 0;
                for (var i = 0; i < zoneLabel.Count(); i++)
                {
                    if (zoneLabel.ElementAt(i).CurrentOffset == offset)
                    {
                        index = i;
                        break;
                    }
                }

                labels.AddRange(zoneLabel.Skip(index));
                return;
            }

            var label = Buffer.GetLabel(CurrentOffset);
            if(label == null)
            {
                CurrentOffset++;
                return;
            }

            CurrentOffset += (UInt16)label.Label.ToBytes().Count();
            labels.Add(label);
            if (maxOffset != null && (CurrentOffset + 2) > maxOffset.Value)
            {
                CurrentOffset++;
                Buffer.Dequeue();
                return;
            }

            InternalNextLabel(labels, maxOffset);
        }
    }
}