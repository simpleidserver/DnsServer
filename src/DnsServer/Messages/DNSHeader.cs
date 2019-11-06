namespace DnsServer.Messages
{
    public class DNSHeader
    {
        /// <summary>
        /// 16 bit identifier assigned by the program that generates any kind of query.
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// Flags
        /// </summary>
        public DNSHeaderFlags Flag { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of entries in the question section.
        /// </summary>
        public uint QdCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the answer section.
        /// </summary>
        public uint AnCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of name server resource records in the authority records section.
        /// </summary>
        public uint NsCount { get; set; }
        /// <summary>
        /// An unsigned 16 bit integer specifying the number of resource records in the additional records section.
        /// </summary>
        public uint ArCount { get; set; }
        /// <summary>
        /// Get the length.
        /// </summary>
        public short Length { get => 12; }

        public static DNSHeader Extract(DNSReadBufferContext context)
        {
            var result = new DNSHeader
            {
                Id = context.NextUInt(),
                Flag = DNSHeaderFlags.Extract(context),
                QdCount = context.NextUInt(),
                AnCount = context.NextUInt(),
                NsCount = context.NextUInt(),
                ArCount = context.NextUInt()
            };
            return result;
        }

        public void Serialize(DNSWriterBufferContext context)
        {
            context.WriteUInt(Id);
            context.WriteFlag(Flag);
            context.WriteUInt(QdCount);
            context.WriteUInt(AnCount);
            context.WriteUInt(NsCount);
            context.WriteUInt(ArCount);
        }
    }
}
