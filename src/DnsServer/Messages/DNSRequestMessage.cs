using System.Collections.Generic;

namespace DnsServer.Messages
{
    public class DNSRequestMessage : DNSMessage
    {
        public DNSRequestMessage()
        {
            Questions = new List<DNSQuestionSection>();
        }

        public ICollection<DNSQuestionSection> Questions { get; set; }

        public static DNSRequestMessage Extract(byte[] buffer)
        {
            var dnsBufferContext = new DNSReadBufferContext(buffer);
            var result = new DNSRequestMessage
            {
                Header = DNSHeader.Extract(dnsBufferContext)
            };
            for(var i = 0; i < result.Header.QdCount; i++)
            {
                result.Questions.Add(DNSQuestionSection.Extract(dnsBufferContext));
            }

            return result;
        }

        public ICollection<byte> Serialize()
        {
            var buffer = new List<byte>();
            var writerContext = new DNSWriterBufferContext(buffer);
            Header.Serialize(writerContext);
            foreach (var question in Questions)
            {
                question.Serialize(writerContext);
            }

            return writerContext.Buffer;
        }
    }
}
