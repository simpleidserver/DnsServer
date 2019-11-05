using System;

namespace DnsServer.Events
{
    public class DnsRequestReceivedEventArgs : EventArgs
    {
        public DnsRequestReceivedEventArgs(byte[] receivedBuffer)
        {
            ReceivedBuffer = receivedBuffer;
        }

        public byte[] ReceivedBuffer { get; set; }
    }
}
