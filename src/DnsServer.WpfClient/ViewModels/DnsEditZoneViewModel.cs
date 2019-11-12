// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Domains;
using DnsServer.Persistence;
using DnsServer.WpfClient.Infrastructures;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace DnsServer.WpfClient.ViewModels
{
    public class DnsResourceRecordResourceTypeViewModel : BindableBase
    {
        private string _name;

        public DnsResourceRecordResourceTypeViewModel(string name, ResourceTypes value)
        {
            _name = name;
            Value = value;
        }

        public string Name
        {
            get
            {
                return _name;

            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public ResourceTypes Value { get; set; }
    }

    public class DnsResourceRecordResourceClassViewModel
    {
        public DnsResourceRecordResourceClassViewModel(string name, ResourceClasses value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public ResourceClasses Value { get; set; }
    }

    public class DnsResourceRecordViewModel
    {
        public DnsResourceRecordViewModel(string resourceType, string resourceClass, int ttl, string content)
        {
            ResourceType = resourceType;
            ResourceClass = resourceClass;
            Ttl = ttl;
            Content = content;
        }

        public string ResourceType { get; set; }
        public string ResourceClass { get; set; }
        public int Ttl { get; set; }
        public string Content { get; set; }
    }

    public class DnsEditZoneViewModel : DialogViewModelBase
    {
        private string _aAddress;
        private string _aaaaAddress;
        private string _cname;
        private string _madName;
        private string _mgmName;
        private string _rmailbx;
        private string _emailbx;
        private string _newName;
        private UInt16 _preference;
        private string _exchange;
        private string _nsdName;
        private string _zoneLabel;
        private string _ptrdName;
        private string _mName;
        private string _rName;
        private uint _serial;
        private int _refresh;
        private int _retry;
        private int _expire;
        private uint _minimum;
        private string _txtData;
        private string _subDomainName;
        private readonly IDnsZoneRepository _dnsZoneRepository;
        private int _ttl;
        private DnsResourceRecordResourceTypeViewModel _selectedResourceType;
        private DnsResourceRecordResourceClassViewModel _selectedResourceClass;

        public DnsEditZoneViewModel(IDnsZoneRepository dnsZoneRepository)
        {
            _dnsZoneRepository = dnsZoneRepository;
            AddZone = new DelegateCommand(HandleAddZone);
            ResourceClasses = new ObservableCollection<DnsResourceRecordResourceClassViewModel>();
            ResourceTypes = new ObservableCollection<DnsResourceRecordResourceTypeViewModel>();
            ResourceRecords = new ObservableCollection<DnsResourceRecordViewModel>();
            foreach(var kvp in DnsServerConstants.DefaultNamesToResourceClasses)
            {
                ResourceClasses.Add(new DnsResourceRecordResourceClassViewModel(kvp.Key, kvp.Value));
            }

            foreach(var kvp in DnsServerConstants.DefaultNameToResourceTypes)
            {
                ResourceTypes.Add(new DnsResourceRecordResourceTypeViewModel(kvp.Key, kvp.Value));
            }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Edit Zone";
            _zoneLabel = parameters.GetValue<string>("Zone");
            _dnsZoneRepository.FindDNSZoneByLabel(_zoneLabel, CancellationToken.None).ContinueWith((r) =>
            {
                var resourceRecords = r.Result.ResourceRecords;
                foreach(var resourceRecord in resourceRecords)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ResourceRecords.Add(new DnsResourceRecordViewModel(
                            DnsServerConstants.DefaultNameToResourceTypes.First(k => k.Value.Equals(resourceRecord.ResourceType)).Key, 
                            DnsServerConstants.DefaultNamesToResourceClasses.First(k => k.Value.Equals(resourceRecord.ResourceClass)).Key, 
                            resourceRecord.Ttl, JsonConvert.SerializeObject(resourceRecord).ToString()));
                    });
                }
            });
        }

        public ICommand AddZone { get; private set; }
        public int Ttl
        {
            get
            {
                return _ttl;
            }
            set
            {
                _ttl = value;
                RaisePropertyChanged("Ttl");
            }
        }
        public string AAddress
        {
            get
            {
                return _aAddress;
            }
            set
            {
                _aAddress = value;
                RaisePropertyChanged("AAddress");
            }
        }
        public string AAAAAddress
        {
            get
            {
                return _aaaaAddress;
            }
            set
            {
                _aaaaAddress = value;
                RaisePropertyChanged("AAAAAddress");
            }
        }
        public string CNAME
        {
            get
            {
                return _cname;
            }
            set
            {
                _cname = value;
                RaisePropertyChanged("CNAME");
            }
        }
        public string MADNAME
        {
            get
            {
                return _madName;
            }
            set
            {
                _madName = value;
                RaisePropertyChanged("MADNAME");
            }
        }
        public string MGMNAME
        {
            get
            {
                return _mgmName;
            }
            set
            {
                _mgmName = value;
                RaisePropertyChanged("MGMNAME");
            }
        }
        public string RMAILBX
        {
            get
            {
                return _rmailbx;
            }
            set
            {
                _rmailbx = value;
                RaisePropertyChanged("RMAILBX");
            }
        }
        public string EMAILBX
        {
            get
            {
                return _emailbx;
            }
            set
            {
                _emailbx = value;
                RaisePropertyChanged("EMAILBX");
            }
        }
        public string NEWNAME
        {
            get
            {
                return _newName;
            }
            set
            {
                _newName = value;
                RaisePropertyChanged("NEWNAME");
            }
        }
        public UInt16 Preference
        {
            get
            {
                return _preference;
            }
            set
            {
                _preference = value;
                RaisePropertyChanged("Preference");
            }
        }
        public string Exchange
        {
            get
            {
                return _exchange;
            }
            set
            {
                _exchange = value;
                RaisePropertyChanged("Exchange");
            }
        }
        public string NSDName
        {
            get
            {
                return _nsdName;
            }
            set
            {
                _nsdName = value;
                RaisePropertyChanged("NSDName");
            }
        }
        public string PTRDNAME
        {
            get
            {
                return _ptrdName;
            }
            set
            {
                _ptrdName = value;
                RaisePropertyChanged("PTRDNAME");
            }
        }
        public string MName
        {
            get
            {
                return _mName;;
            }
            set
            {
                _mName = value;
                RaisePropertyChanged("MName");
            }
        }
        public string RName
        {
            get
            {
                return _rName;
            }
            set
            {
                _rName = value;
                RaisePropertyChanged("RName");
            }
        }
        public uint Serial
        {
            get
            {
                return _serial;
            }
            set
            {
                _serial = value;
                RaisePropertyChanged("Serial");
            }
        }
        public int Refresh
        {
            get
            {
                return _refresh;
            }
            set
            {
                _refresh = value;
                RaisePropertyChanged("Refresh");
            }
        }
        public int Retry
        {
            get
            {
                return _retry;
            }
            set
            {
                _retry = value;
                RaisePropertyChanged("Retry");
            }
        }
        public int Expire
        {
            get
            {
                return _expire;
            }
            set
            {
                _retry = value;
                RaisePropertyChanged("Expire");
            }
        }
        public uint Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;
                RaisePropertyChanged("Minimum");
            }
        }
        public string TxtData
        {
            get
            {
                return _txtData;
            }
            set
            {
                _txtData = value;
                RaisePropertyChanged("TxtData");
            }
        }
        public string SubDomainName
        {
            get
            {
                return _subDomainName;
            }
            set
            {
                _subDomainName = value;
                RaisePropertyChanged("SubDomainName");
            }
        }
        public DnsResourceRecordResourceTypeViewModel SelectedResourceType
        {
            get
            {
                return _selectedResourceType;
            }
            set
            {
                _selectedResourceType = value;
                RaisePropertyChanged("SelectedResourceType");
            }
        }
        public DnsResourceRecordResourceClassViewModel SelectedResourceClass
        {
            get
            {
                return _selectedResourceClass;
            }
            set
            {
                _selectedResourceClass = value;
                RaisePropertyChanged("SelectedResourceClass");
            }
        }
        public ObservableCollection<DnsResourceRecordResourceClassViewModel> ResourceClasses { get; private set; }
        public ObservableCollection<DnsResourceRecordResourceTypeViewModel> ResourceTypes { get; private set; }
        public ObservableCollection<DnsResourceRecordViewModel> ResourceRecords { get; private set; }

        private void HandleAddZone()
        {
            _dnsZoneRepository.FindDNSZoneByLabel(_zoneLabel, CancellationToken.None).ContinueWith((r) =>
            {
                var zone = r.Result;
                var newResourceRecord = BuildResourceRecord();
                zone.ResourceRecords.Add(newResourceRecord);
                _dnsZoneRepository.UpdateZone(zone).ContinueWith((sr) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ResourceRecords.Add(new DnsResourceRecordViewModel(SelectedResourceType.Name, SelectedResourceClass.Name, Ttl, JsonConvert.SerializeObject(newResourceRecord).ToString()));
                        Ttl = 0;
                        AAAAAddress = string.Empty;
                        AAddress = string.Empty;
                        MADNAME = string.Empty;
                        MGMNAME = string.Empty;
                        RMAILBX = string.Empty;
                        EMAILBX = string.Empty;
                        NEWNAME = string.Empty;
                        Preference = 0;
                        Exchange = string.Empty;
                        NSDName = string.Empty;
                        PTRDNAME = string.Empty;
                        TxtData = string.Empty;
                        SubDomainName = string.Empty;
                        SelectedResourceClass = ResourceClasses.First();
                        SelectedResourceType = ResourceTypes.First();
                    });
                });
            });
        }

        private ResourceRecord BuildResourceRecord()
        {
            ResourceRecord result = null;
            if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.A))
            {
                var tmp = new AResourceRecord(Ttl);
                tmp.Address = AAddress;
                tmp.SubZoneName = SubDomainName;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.AAAA))
            {
                var tmp = new AAAAResourceRecord(Ttl);
                tmp.Address = AAAAAddress;
                tmp.SubZoneName = SubDomainName;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.CNAME))
            {
                var tmp = new CNAMEResourceRecord(Ttl);
                tmp.CNAME = CNAME;
                result = tmp;
            } else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.MB))
            {
                var tmp = new MBResourceRecord(Ttl);
                tmp.MADNAME = MADNAME;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.MG))
            {
                var tmp = new MGResourceRecord(Ttl);
                tmp.MGMNAME = MGMNAME;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.MINFO))
            {
                var tmp = new MINFOResourceRecord(Ttl);
                tmp.RMAILBX = RMAILBX;
                tmp.EMAILBX = EMAILBX;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.MR))
            {
                var tmp = new MRResourceRecord(Ttl);
                tmp.NEWNAME = NEWNAME;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.MX))
            {
                var tmp = new MXResourceRecord(Ttl);
                tmp.Preference = Preference;
                tmp.Exchange = Exchange;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.NS))
            {
                var tmp = new NSResourceRecord(Ttl);
                tmp.NSDName = NSDName;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.PTR))
            {
                var tmp = new PTRResourceRecord(Ttl);
                tmp.PTRDNAME = PTRDNAME;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.SOA))
            {
                var tmp = new SOAResourceRecord(Ttl);
                tmp.MName = MName;
                tmp.RName = RName;
                tmp.Serial = Serial;
                tmp.Refresh = Refresh;
                tmp.Retry = Retry;
                tmp.Expire = Expire;
                tmp.Minimum = Minimum;
                result = tmp;
            }
            else if (SelectedResourceType.Value.Equals(Domains.ResourceTypes.TXT))
            {
                var tmp = new TXTResourceRecord(Ttl);
                tmp.TxtData = TxtData;
                result = tmp;
            }

            UpdateResourceRecord(result);
            return result;
        }

        private void UpdateResourceRecord(ResourceRecord resourceRecord)
        {
            resourceRecord.Ttl = Ttl;
            resourceRecord.ResourceClass = SelectedResourceClass.Value;
            resourceRecord.ResourceType = SelectedResourceType.Value;
        }
    }
}
