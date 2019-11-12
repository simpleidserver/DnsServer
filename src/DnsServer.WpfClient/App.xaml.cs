// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Persistence;
using DnsServer.Persistence.InMemory;
using DnsServer.WpfClient.ViewModels;
using DnsServer.WpfClient.Views;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Prism.Ioc;
using System.Windows;

namespace DnsServer.WpfClient
{
    internal partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DnsZonesView, DnsZonesViewModel>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<DnsEditZoneView, DnsEditZoneViewModel>();
            containerRegistry.RegisterForNavigation<DnsEditConfigurationView, DnsEditConfigurationViewModel>();
            containerRegistry.RegisterInstance(typeof(IOptions<DnsServerOptions>), Options.Create(DnsServerWpfClientConstants.Options));
            containerRegistry.Register<IDnsServerHost, DnsServerHost>();
            containerRegistry.Register<IDnsAuthoritativeHandler, DnsAuthoritativeHandler>();
            containerRegistry.Register<IDnsRecursiveHandler, DnsRecursiveHandler>();
            containerRegistry.Register<IDnsResolver, DnsResolver>();
            containerRegistry.RegisterInstance(typeof(IDnsRootServerRepository), new InMemoryDnsRootServerRepository(DnsServerConstants.DefaultRootServers));
            containerRegistry.RegisterInstance(typeof(IDnsZoneRepository), new InMemoryDnsZoneRepository(DnsServerWpfClientConstants.DefaultDNSZones));
            containerRegistry.RegisterSingleton<IDistributedCache, MemoryDistributedCache>();
            containerRegistry.RegisterInstance(typeof(IOptions<MemoryDistributedCacheOptions>), Options.Create(new MemoryDistributedCacheOptions()));
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}