How to setup an DNS server ?
============================

A DNS server can be hosted in any DOTNET CORE project like ASP.NET CORE or Console application. 
Follow the steps below to deploy a DNS server into a console application :

1) Create an empty Console application.

2) Install the Nuget package **DnsServer**.

3) In the Program.cs file, insert the following line into the **Main** method :

	var dnsServer = new DnsServerHostBuilder(o =>
        {
            o.ExcludeForwardRequests.Add(new Regex("^.*example\\.com$"));
            o.ExcludeForwardRequests.Add(new Regex("^.*in-addr\\.arpa$"));
        })
        .AddDNSZones(new List<DNSZone>
        {
            new DNSZone("example.com")
            {
                ResourceRecords = new List<ResourceRecord>
                {
                    new AResourceRecord(3600)
                    {
                        Address = "127.0.0.1"
                    },
                    new AResourceRecord(3600, "www")
                    {
                        Address = "127.0.0.1"
                    },
                    new AResourceRecord(3600, "ns1")
                    {
                        Address = "127.0.0.1"
                    },
                    new SOAResourceRecord(3600)
                    {
                        MName = "ns1.example.com",
                        RName = "admin.example.com",
                        Serial = 5,
                        Refresh = 604800,
                        Expire = 2419200,
                        Minimum = 604800,
                        Retry = 86400
                    },
                    new NSResourceRecord(3600)
                    {
                        NSDName = "ns1.example.com"
                    }
                }
            },
            new DNSZone("1.0.0.127.in-addr.arpa")
            {
                ResourceRecords = new List<ResourceRecord>
                {
                    new PTRResourceRecord(100)
                    {
                        PTRDNAME = "localhost"
                    }
                }
            }
        })
        .AddDNSRootServers(DnsServerConstants.DefaultRootServers)
        .Build();
	dnsServer.Run();
	
4) Configure the network interface to use the local DNS server.

5) Open a command prompt and execute **ping www.example.com**.

The **DnsServerHostBuilder** class accepts in its constructor a callback which can be used by developers to change the options of the DNS server.
There are several operations exposed by the **DnsServerHostBuilder** class :

- **AddDNSZones** : Configure DNS zones.

- **AddDNSRootServers** : Configure the DNS root servers.

A WPF client also exists `here`_.

.. _here: https://github.com/simpleidserver/DnsServer/tree/master/src/DnsServer.WpfClient