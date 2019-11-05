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

        public static DNSRequestMessage Extract(Queue<byte> buffer)
        {
            var result = new DNSRequestMessage
            {
                Header = DNSHeader.Extract(buffer)
            };
            for(var i = 0; i < result.Header.QdCount; i++)
            {
                result.Questions.Add(DNSQuestionSection.Extract(buffer));
            }

            return result;
        }
    }
}
