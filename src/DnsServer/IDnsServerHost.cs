using DnsServer.Events;
using System;

namespace DnsServer
{
    public interface IDnsServerHost
    {
        bool IsRunning { get; }
        void Run(string ipAddr = "127.0.0.1", int port = 53);
        void Stop();
        event EventHandler<EventArgs> DnsServerStarted;
        event EventHandler<DnsRequestReceivedEventArgs> DnsRequestReceived;
        event EventHandler<DnsResponseSentEventArgs> DnsResponseSent;
        event EventHandler<EventArgs> DnsServerStopped;
    }
}
