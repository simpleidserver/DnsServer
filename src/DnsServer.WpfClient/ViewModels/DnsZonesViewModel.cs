// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Persistence;
using DnsServer.WpfClient.Infrastructures;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace DnsServer.WpfClient.ViewModels
{
    public class DnsZoneViewModel
    {
        public DnsZoneViewModel(string label)
        {
            Label = label;
        }

        public string Label { get; set; }    
    }

    public class DnsZonesViewModel : DialogViewModelBase
    {
        private string _zoneName;
        private DnsZoneViewModel _selectedZone;
        private readonly IDnsZoneRepository _dnsZoneRepository;
        private readonly IDialogService _dialogService;

        public DnsZonesViewModel(IDnsZoneRepository dnsZoneRepository, IDialogService dialogService)
        {
            _dnsZoneRepository = dnsZoneRepository;
            _dialogService = dialogService;
            AddZone = new DelegateCommand(HandleAddZone);
            EditZone = new DelegateCommand(HandleEditZone);
            Zones = new ObservableCollection<DnsZoneViewModel>();
        }

        public ObservableCollection<DnsZoneViewModel> Zones { get; set; }
        public ICommand AddZone { get; private set; }
        public ICommand EditZone { get; private set; }
        public string ZoneName
        {
            get
            {
                return _zoneName;
            }
            set
            {
                _zoneName = value;
                RaisePropertyChanged("ZoneName");
            }
        }
        public DnsZoneViewModel SelectedZone
        {
            get
            {
                return _selectedZone;
            }
            set
            {
                _selectedZone = value;
                RaisePropertyChanged("SelectedZone");
            }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "DNS Zones";
            _dnsZoneRepository.FindAll().ContinueWith((r) =>
            {
                foreach (var record in r.Result)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Zones.Add(new DnsZoneViewModel(record.ZoneLabel));
                    });
                }
            });
        }

        private void HandleAddZone()
        {
            _dnsZoneRepository.AddZone(_zoneName, CancellationToken.None).ContinueWith((r) =>
            {
                var isAdded = r.Result;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Zones.Add(new DnsZoneViewModel(_zoneName));
                    ZoneName = string.Empty;
                });
            });
        }

        private void HandleEditZone()
        {
            _dialogService.Show("DnsEditZoneView", new DialogParameters($"Zone={_selectedZone.Label}"), result => { });
        }
    }
}