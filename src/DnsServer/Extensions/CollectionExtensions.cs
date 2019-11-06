using DnsServer.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnsServer.Extensions
{
    public static class CollectionExtensions
    {
        public static ICollection<byte> ConvertLabelToBytes(this string str)
        {
            var result = new List<byte>();
            var labels = str.Split('.');
            foreach (var label in labels)
            {
                var payload = Encoding.ASCII.GetBytes(label);
                result.Add((byte)payload.Count());
                result.AddRange(payload);
            }

            result.Add(0x00);
            return result;
        }

        public static ICollection<byte> ToBytes(this string str)
        {
            var result = new List<byte>();
            var payload = Encoding.ASCII.GetBytes(str);
            result.Add((byte)payload.Count());
            result.AddRange(payload);
            return result;
        }

        public static ICollection<byte> ToBytes(this uint sh)
        {
            var result = new List<byte>
            {
                (byte)(sh >> 8),
                (byte)(sh & 0xFF)
            };
            return result;
        }

        public static ICollection<byte> ToBytes(this short sh)
        {
            var result = new List<byte>
            {
                (byte)(sh >> 8),
                (byte)(sh & 0xFF)
            };
            return result;
        }

        public static ICollection<byte> ToBytes(this int it)
        {
            var result = new List<byte>
            {
                (byte)(it >> 24),
                (byte)((it >> 16) & 0xFF),
                (byte)((it >> 8) & 0xFF),
                (byte)(it & 0xFF)
            };
            return result;
        }

        public static short GetShort(this Queue<byte> queue, bool dequeue = true)
        {
            var payload = queue.Take(2);
            if (dequeue)
            {
                payload = queue.Dequeue(2);
            }

            return ConvertToShort(payload);
        }

        public static uint GetUInt(this Queue<byte> queue, bool dequeue = true)
        {
            var payload = queue.Take(2);
            if (dequeue)
            {
                payload = queue.Dequeue(2);
            }

            return ConvertToUInt(payload);
        }

        public static int GetInt(this Queue<byte> queue)
        {
            var payload = queue.Dequeue(4);
            var result = ((payload.ElementAt(0) & 0xFF) << 24) | ((payload.ElementAt(1) & 0xFF) << 16) | ((payload.ElementAt(2) & 0xFF) << 8) | (0xFF & payload.ElementAt(3));
            return result;
        }

        public static DNSZoneLabel GetLabel(this Queue<byte> queue, uint currentOffset)
        {
            var size = queue.Dequeue();
            if (size == 0)
            {
                return null;
            }

            return new DNSZoneLabel(Encoding.ASCII.GetString(queue.Dequeue(size).ToArray()), currentOffset);
        }

        public static IEnumerable<byte> Dequeue(this Queue<byte> queue, uint number)
        {
            var result = new List<byte>();
            for(var i = 0; i < number; i++)
            {
                result.Add(queue.Dequeue());
            }

            return result.ToArray();
        }

        private static short ConvertToShort(IEnumerable<byte> payload)
        {
            var result = (payload.ElementAt(0) & 0xFF) << 8 | (payload.ElementAt(1) & 0xFF);
            return (short)result;
        }

        private static uint ConvertToUInt(IEnumerable<byte> payload)
        {
            var result = (payload.ElementAt(0) & 0xFF) << 8 | (payload.ElementAt(1) & 0xFF);
            return (uint)result;
        }

        private static void InternalGetLabel(Queue<byte> queue, List<DNSZoneLabel> labels, uint currentOffset)
        {
            var size = queue.Dequeue();
            if (size == 0)
            {
                return;
            }

            labels.Add(new DNSZoneLabel(Encoding.ASCII.GetString(queue.Dequeue(size).ToArray()), currentOffset));
            currentOffset += (uint)(1 + size);
            InternalGetLabel(queue, labels, currentOffset);
        }
    }
}
