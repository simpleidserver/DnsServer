using System;

namespace DnsServer.Messages.Builders
{
    public class DNSRequestMessageBuilder
    {
        public DNSRequestMessageQuestionBuilder New()
        {
            var random = new Random();
            return New((uint)random.Next(short.MinValue, short.MaxValue));
        }

        public DNSRequestMessageQuestionBuilder New(uint id)
        {
            var dnsRequestMessage = new DNSRequestMessage
            {
                Header = new DNSHeader
                {
                    Id = id,
                    Flag = DNSHeaderFlags.STANDARD_QUERY
                }
            };
            return new DNSRequestMessageQuestionBuilder(dnsRequestMessage);
        }   
    }
}
