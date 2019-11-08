// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public interface IDnsResolver
    {
        Task<DNSResponseMessage> Resolve(string zoneName, ResourceClasses resourceClass, ResourceTypes resourceType, CancellationToken cancellationToken);
    }
}
