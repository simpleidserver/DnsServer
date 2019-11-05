using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Messages
{
    public class DNSHeader
    {
        /// <summary>
        /// 16 bit identifier assigned by the program that generates any kind of query.
        /// </summary>
        public short Id { get; set; }
        /// <summary>
        /// Flags
        /// </summary>
        public DNSHeaderFlags Flag { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of entries in the question section.
        /// </summary>
        public short QdCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the answer section.
        /// </summary>
        public short AnCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of name server resource records in the authority records section.
        /// </summary>
        public short NsCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the additional records section.
        /// </summary>
        public short ArCount { get; set; }
        /// <summary>
        /// Get the length.
        /// </summary>
        public short Length { get => 12; }

        public static DNSHeader Extract(Queue<byte> buffer)
        {
            var result = new DNSHeader
            {
                Id = buffer.GetShort(),
                Flag = DNSHeaderFlags.Extract(buffer),
                QdCount = buffer.GetShort(),
                AnCount = buffer.GetShort(),
                NsCount = buffer.GetShort(),
                ArCount = buffer.GetShort()
            };
            return result;
        }

        public ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            result.AddRange(Id.ToBytes());
            result.AddRange(Flag.ToBytes());
            result.AddRange(QdCount.ToBytes());
            result.AddRange(AnCount.ToBytes());
            result.AddRange(NsCount.ToBytes());
            result.AddRange(ArCount.ToBytes());
            return result;
        }
    }
}
