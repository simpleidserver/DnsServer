using DnsServer.Events;
using DnsServer.Extensions;
using DnsServer.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsServerHost
    {
        private readonly UdpClient _udpClient;
        private readonly CancellationToken _cancellationToken;
        private readonly CancellationTokenSource _tokenSource;
        private readonly ServiceProvider _serviceProvider;

        public event EventHandler<EventArgs> DnsServerStarted;
        public event EventHandler<DnsRequestReceivedEventArgs> DnsRequestReceived;
        public event EventHandler<EventArgs> DnsServerStopped;

        internal DnsServerHost(UdpClient udpClient, ServiceProvider serviceProvider)
        {
            _udpClient = udpClient;
            _tokenSource = new CancellationTokenSource();
            _cancellationToken = _tokenSource.Token;
            _serviceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider => _serviceProvider;

        public void Run()
        {
            Task.Run(async () => await InternalRun());
        }

        public void Stop()
        {
            _tokenSource.Cancel();
        }

        private async Task InternalRun()
        {
            if (DnsServerStarted != null)
            {
                DnsServerStarted(this, new EventArgs());
            }

            try
            {
                while (true)
                {
                    _cancellationToken.ThrowIfCancellationRequested();
                    await HandleDNSRequest();
                }
            }
            catch(OperationCanceledException)
            {
                DnsServerStopped(this, new EventArgs());
            }
        }

        private async Task HandleDNSRequest()
        {
            UdpReceiveResult receiveResult;
            try
            {

                receiveResult = await _udpClient.ReceiveAsync().WithCancellation(_cancellationToken);
            }
            catch(SocketException)
            {
                return;
            }

            var requestMessage = DNSRequestMessage.Extract(receiveResult.Buffer);
            if (!requestMessage.Questions.Any(q => q.Label == "google.com"))
            {
                return;
            }

            if (DnsRequestReceived != null)
            {
                DnsRequestReceived(this, new DnsRequestReceivedEventArgs(requestMessage));
            }

            var dnsRequestHandler = _serviceProvider.GetService<IDnsRequestHandler>();
            var dnsResponse = await dnsRequestHandler.Handle(requestMessage, _cancellationToken);
            var response = dnsResponse.Serialize();
            await _udpClient.SendAsync(response.ToArray(), response.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken);
        }
    }
}