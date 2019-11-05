using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Domains
{
    public class BaseDomainEnum
    {
        public BaseDomainEnum(short value)
        {
            Value = value;
        }

        public short Value { get; private set; }

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
            return ((short)(Value)).ToBytes();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
