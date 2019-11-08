// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Extensions;
using System;
using System.Collections.Generic;

namespace DnsServer.Domains
{
    public class BaseDomainEnum
    {
        public BaseDomainEnum(UInt16 value)
        {
            Value = value;
        }

        public UInt16 Value { get; private set; }

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
