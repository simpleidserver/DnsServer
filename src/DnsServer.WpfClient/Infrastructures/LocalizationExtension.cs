// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Windows.Data;

namespace DnsServer.WpfClient.Infrastructures
{
    public class LocalizationExtension : Binding
    {
        public LocalizationExtension() : this(string.Empty) { }

        public LocalizationExtension(string name) : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}
