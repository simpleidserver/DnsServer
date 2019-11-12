// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.WpfClient.Infrastructures;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace DnsServer.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
            LoadCommand = new DelegateCommand(HandleLoadCommand);
            ExitCommand = new DelegateCommand(HandleExitCommand);
            DnsZonesCommand = new DelegateCommand(HandleDnsZonesCommand);
            DnsEditConfigurationCommand = new DelegateCommand(HandleDnsEditConfigurationCommand);
            ChangeLanguageCommand = new DelegateCommand<string>(HandleChangeLanguageCommand);
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand DnsZonesCommand { get; private set; }
        public ICommand ChangeLanguageCommand { get; private set; }
        public ICommand DnsEditConfigurationCommand { get; private set; }

        private void HandleLoadCommand()
        {
            _regionManager.RequestNavigate("ContentRegion", "HomeView");
        }

        private void HandleExitCommand()
        {
            Application.Current.Shutdown();
        }

        private void HandleDnsZonesCommand()
        {
            _dialogService.Show("DnsZonesView", new DialogParameters("Title=Good Title"), result => { });
        }

        private void HandleDnsEditConfigurationCommand()
        {
            _dialogService.Show("DnsEditConfigurationView", new DialogParameters("Title=Good Title"), result => { });
        }

        private void HandleChangeLanguageCommand(string language)
        {
            var ci = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            TranslationSource.Instance.CurrentCulture = ci;
        }
    }
}
