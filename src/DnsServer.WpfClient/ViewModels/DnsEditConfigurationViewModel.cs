// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.WpfClient.Infrastructures;
using Microsoft.Extensions.Options;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace DnsServer.WpfClient.ViewModels
{
    public class DnsEditConfigurationViewModel : DialogViewModelBase
    {
        private readonly DnsServerOptions _options;
        private string _regularExpression;
        private int _timeOutInMilliSeconds;
        private string _defaultCpu;
        private string _defaultOS;
        private int _defaultTtl;

        public DnsEditConfigurationViewModel(IOptions<DnsServerOptions> options)
        {
            _options = options.Value;
            UpdateConfiguration = new DelegateCommand(HandleUpdateConfiguration);
            AddExcludeForwardRequest = new DelegateCommand(HandleAddExcludeForwardRequest);
            RegularExpressions = new ObservableCollection<string>();
        }

        public ObservableCollection<string> RegularExpressions { get; private set; }
        public ICommand UpdateConfiguration { get; private set; }
        public ICommand AddExcludeForwardRequest { get; private set; }

        public string RegularExpression
        {
            get
            {
                return _regularExpression;
            }
            set
            {
                _regularExpression = value;
                RaisePropertyChanged("RegularExpression");
            }
        }

        public int TimeOutInMilliSeconds
        {
            get
            {
                return _timeOutInMilliSeconds;
            }
            set
            {
                _timeOutInMilliSeconds = value;
                RaisePropertyChanged("TimeOutInMilliSeconds");
            }
        }

        public string DefaultCpu
        {
            get
            {
                return _defaultCpu;
            }
            set
            {
                _defaultCpu = value;
                RaisePropertyChanged("DefaultCpu");
            }
        }

        public string DefaultOS
        {
            get
            {
                return _defaultOS;
            }
            set
            {
                _defaultOS = value;
                RaisePropertyChanged("DefaultOS");
            }
        }

        public int DefaultTtl
        {
            get
            {
                return _defaultTtl;
            }
            set
            {
                _defaultTtl = value;
                RaisePropertyChanged("DefaultTtl");
            }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            TimeOutInMilliSeconds = _options.TimeOutInMilliSeconds;
            DefaultCpu = _options.DefaultCpu;
            DefaultOS = _options.DefaultOS;
            DefaultTtl = _options.DefaultTtl;
            foreach(var r in _options.ExcludeForwardRequests)
            {
                RegularExpressions.Add(r.ToString());
            }
        }

        private void HandleUpdateConfiguration()
        {
            _options.TimeOutInMilliSeconds = TimeOutInMilliSeconds;
            _options.DefaultCpu = DefaultCpu;
            _options.DefaultOS = DefaultOS;
            _options.DefaultTtl = DefaultTtl;
        }

        private void HandleAddExcludeForwardRequest()
        {
            RegularExpressions.Add(RegularExpression);
            _options.ExcludeForwardRequests.Add(new Regex(RegularExpression));
            RegularExpression = string.Empty;
        }
    }
}