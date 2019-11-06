using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Messages
{
    public class DNSHeaderFlags
    {
        public static DNSHeaderFlags RESPONSE = new DNSHeaderFlags(0x8000);
        public static DNSHeaderFlags INVERSE_QUERY = new DNSHeaderFlags(0x0800);
        public static DNSHeaderFlags SERVER_STATUS = new DNSHeaderFlags(0x1000);
        public static DNSHeaderFlags AUTHORITATIVE_ANSWER = new DNSHeaderFlags(0x0400);
        public static DNSHeaderFlags TRUNCATED = new DNSHeaderFlags(0x0200);
        public static DNSHeaderFlags STANDARD_QUERY = new DNSHeaderFlags(0x0100);
        public static DNSHeaderFlags RECURSION_AVAILABLE = new DNSHeaderFlags(0x0080);
        /// <summary>
        /// The name server was unable to interpret the query.
        /// </summary>
        public static DNSHeaderFlags FORMAT_ERROR = new DNSHeaderFlags(0x0001);
        /// <summary>
        /// The name server was unable to process this query due to a problem with the name server.
        /// </summary>
        public static DNSHeaderFlags SERVER_FAILURE = new DNSHeaderFlags(0x0002);
        /// <summary>
        /// Meaningful only for responses from an authoritative name server, this code
        /// signifies that the domain name referenced in the query does not exist.
        /// </summary>
        public static DNSHeaderFlags NAME_ERROR = new DNSHeaderFlags(0x0003);
        /// <summary>
        /// The name server does not support the requested kind of query.
        /// </summary>
        public static DNSHeaderFlags NOT_IMPLEMENTED = new DNSHeaderFlags(0x0004);
        /// <summary>
        /// The name server refuses to perform the specified operation for policy reasons.
        /// For example, a name server may not wish to provide the information to the particular requester,
        /// or a name server may not wish to perform a particular operation for particular data.
        /// </summary>
        public static DNSHeaderFlags REFUSED = new DNSHeaderFlags(0x0005);

        public DNSHeaderFlags() { }

        public DNSHeaderFlags(uint value)
        {
            Value = value;
        }

        public void SetFlag(DNSHeaderFlags flag)
        {
            SetFlag(flag.Value);
        }

        public void SetFlag(uint flag)
        {
            Value = Value | flag;
        }

        public bool IsFlagSet(DNSHeaderFlags flag)
        {
            return IsFlagSet(flag.Value);
        }

        public bool IsFlagSet(long flag)
        {
            return (Value & flag) != 0;
        }

        public ICollection<byte> ToBytes()
        {
            return Value.ToBytes();
        }

        public static DNSHeaderFlags Extract(DNSReadBufferContext buffer)
        {
            return new DNSHeaderFlags(buffer.NextUInt());
        }

        public uint Value { get; private set; }
    }
}
