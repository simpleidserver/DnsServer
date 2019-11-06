using DnsServer.Extensions;
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
        public uint CurrentOffset { get; private set; }

        public IEnumerable<byte> NextBytes(uint length)
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

        public short NextShort()
        {
            var result = Buffer.GetShort();
            CurrentOffset += 2;
            return result;
        }

        public int NextInt()
        {
            var result = Buffer.GetInt();
            CurrentOffset += 4;
            return result;
        }

        public uint NextUInt()
        {
            var result = Buffer.GetUInt();
            CurrentOffset += 2;
            return result;
        }

        public string NextLabel()
        {
            List<DNSZoneLabel> newLabels = new List<DNSZoneLabel>();
            List<DNSZoneLabel> labels = new List<DNSZoneLabel>();
            InternalNextLabel(newLabels, labels);
            ZoneLabels.Add(newLabels);
            return string.Join(".", labels.Select(l => l.Label));
        }

        private void InternalNextLabel(List<DNSZoneLabel> newLabels, List<DNSZoneLabel> labels)
        {
            var offset = Buffer.GetUInt(false);
            if (offset >= 0xc000)
            {
                offset = (uint)(NextUInt() ^ 0xc000);
                labels.AddRange(ZoneLabels.First(m => m.Any(z => z.CurrentOffset == offset)).Where(z => z.CurrentOffset >= offset));
                return;
            }

            var label = Buffer.GetLabel(CurrentOffset);
            if(label == null)
            {
                CurrentOffset++;
                return;
            }

            CurrentOffset += (uint)label.Label.ToBytes().Count();
            labels.Add(label);
            newLabels.Add(label);
            InternalNextLabel(newLabels, labels);
        }
    }
}