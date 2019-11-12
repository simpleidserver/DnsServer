// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using DnsServer.Events;
using DnsServer.Exceptions;
using DnsServer.Extensions;
using DnsServer.Messages;
using DnsServer.Messages.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DnsServer
{
    public class DnsServerHost : IDnsServerHost
    {
        private UdpClient _udpClient;
        private readonly DnsServerOptions _options;
        private readonly IDnsRecursiveHandler _recursiveHandler;
        private readonly IDnsAuthoritativeHandler _authoritativeHandler;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _tokenSource;

        public event EventHandler<EventArgs> DnsServerStarted;
        public event EventHandler<DnsRequestReceivedEventArgs> DnsRequestReceived;
        public event EventHandler<DnsResponseSentEventArgs> DnsResponseSent;
        public event EventHandler<EventArgs> DnsServerStopped;

        public DnsServerHost(IOptions<DnsServerOptions> options, IDnsRecursiveHandler recursiveHandler, IDnsAuthoritativeHandler authoritativeHandler)
        {
            _options = options.Value;
            _recursiveHandler = recursiveHandler;
            _authoritativeHandler = authoritativeHandler;
        }

        public bool IsRunning { get; private set; }

        public void Run(string ipAddr = "127.0.0.1", int port = 53)
        {
            _tokenSource = new CancellationTokenSource();
            _cancellationToken = _tokenSource.Token;
            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(ipAddr), port));
            IsRunning = true;
            Task.Run(async () => await InternalRun());
        }

        public void Stop()
        {
            IsRunning = false;
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
                _udpClient.Close();
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

            var builder = new DNSResponseMessageBuilder();
            DNSRequestMessage requestMessage = null;
            try
            {
                requestMessage = DNSRequestMessage.Extract(receiveResult.Buffer);
                Validate(requestMessage);
            }
            catch (DNSNotImplementedException)
            {
                var payload = builder.BuildNotImplemented(requestMessage).Serialize();
                await _udpClient.SendAsync(payload.ToArray(), payload.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
                return;
            }
            catch (DNSRefusedException)
            {
                var payload = builder.BuildRefused(requestMessage).Serialize();
                await _udpClient.SendAsync(payload.ToArray(), payload.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
                return;
            }
            catch
            {
                var payload = builder.BuildFormatError(requestMessage).Serialize();
                await _udpClient.SendAsync(payload.ToArray(), payload.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
                return;
            }

            if (DnsRequestReceived != null)
            {
                DnsRequestReceived(this, new DnsRequestReceivedEventArgs(requestMessage));
            }

            DNSResponseMessage dnsResponseMessage = null;
            try
            {
                if (!_options.ExcludeForwardRequests.Any(r => r.IsMatch(requestMessage.Questions.First().Label)))
                {
                    dnsResponseMessage = await _recursiveHandler.Handle(requestMessage, _cancellationToken);
                }
                else
                {
                    dnsResponseMessage = await _authoritativeHandler.Handle(requestMessage, _cancellationToken);
                }
            }
            catch(DNSNameErrorException)
            {
                var payload = builder.BuildNameError(requestMessage).Serialize();
                await _udpClient.SendAsync(payload.ToArray(), payload.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
                return;
            }
            catch
            {
                var payload = builder.BuildServerFailure(requestMessage).Serialize();
                await _udpClient.SendAsync(payload.ToArray(), payload.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
                return;
            }

            var response = dnsResponseMessage.Serialize();
            await _udpClient.SendAsync(response.ToArray(), response.Count(), receiveResult.RemoteEndPoint).WithCancellation(_cancellationToken, _options.TimeOutInMilliSeconds);
            if (DnsResponseSent != null)
            {
                DnsResponseSent(this, new DnsResponseSentEventArgs(dnsResponseMessage));
            }
        }

        private void Validate(DNSRequestMessage requestMessage)
        {
            if (requestMessage.Questions.Count() != 1)
            {
                throw new DNSRefusedException();
            }

            foreach(var question in requestMessage.Questions)
            {
                if (!DnsServerConstants.DefaultQuestionTypes.Any(s => s.Equals(question.QType)))
                {
                    throw new DNSNotImplementedException();
                }

                if (!DnsServerConstants.DefaultQuestionClasses.Any(s => s.Equals(question.QClass)))
                {
                    throw new DNSBadFormatException();
                }
            }
        }
    }
}