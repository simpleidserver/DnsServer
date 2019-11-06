using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Domains
{
    public class BaseDomainEnum
    {
        public BaseDomainEnum(uint value)
        {
            Value = value;
        }

        public uint Value { get; private set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.GetHashCode().Equals(obj.GetHashCode());
        }

        public ICollection<byte> ToBytes()
        {
            return Value.ToBytes();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
