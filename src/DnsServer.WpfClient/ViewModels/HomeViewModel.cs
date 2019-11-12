// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Prism.Commands;
using Prism.Mvvm;
using System.Text;
using System.Windows.Input;

namespace DnsServer.WpfClient.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private StringBuilder _dnsOutputBuilder;
        private readonly IDnsServerHost _dnsServerHost;
        private bool _isDnsServerStarted;
        private string _dnsOutput;

        public HomeViewModel(IDnsServerHost dnsServerHost)
        {
            _dnsServerHost = dnsServerHost;
            _dnsServerHost.DnsServerStarted += HandleDnsServerStarted;
            _dnsServerHost.DnsServerStopped += HandleDnsServerStopped;
            _isDnsServerStarted = false;
            ToggleDnsServer = new DelegateCommand(HandleToggleDnsServer);
            _dnsOutputBuilder = new StringBuilder();
        }

        public ICommand ToggleDnsServer { get; private set; }

        public string DnsOutput
        {
            get { return _dnsOutput; }
            set
            {
                _dnsOutput = value;
                RaisePropertyChanged("DnsOutput");
            }
        }

        public bool IsDnsServerStarted
        {
            get { return _isDnsServerStarted; }
            set 
            {
                _isDnsServerStarted = value;
                RaisePropertyChanged("IsDnsServerStarted");
            }
        }

        private void HandleToggleDnsServer()
        {
            if (_dnsServerHost.IsRunning)
            {
                _dnsServerHost.Stop();
            }
            else
            {
                _dnsServerHost.Run();
            }
        }

        private void HandleDnsServerStarted(object sender, System.EventArgs e)
        {
            IsDnsServerStarted = true;
            _dnsOutputBuilder.AppendLine("DNS server started");
            UpdateDnsOutput();
        }

        private void HandleDnsServerStopped(object sender, System.EventArgs e)
        {
            IsDnsServerStarted = false;
            _dnsOutputBuilder.AppendLine("DNS server stopped");
            UpdateDnsOutput();
        }

        private void UpdateDnsOutput()
        {
            DnsOutput = _dnsOutputBuilder.ToString();
        }
    }
}
