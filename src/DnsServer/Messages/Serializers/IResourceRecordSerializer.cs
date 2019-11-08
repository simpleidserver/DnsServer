// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;

namespace DnsServer.Messages.Serializers
{
    public interface IResourceRecordSerializer
    {
        ResourceTypes ResourceType { get; }
        DNSResourceRecord Extract(DNSReadBufferContext context, string name, ResourceClasses resourceClass, int ttl);
        void Serialize(DNSWriterBufferContext context, DNSResourceRecord resourceRecord);
    }
}
