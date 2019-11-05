using DnsServer.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DnsServer.Messages
{
    public class DNSResponseMessage : DNSMessage
    {
        public DNSResponseMessage()
        {
            Questions = new List<DNSQuestionSection>();
            Answers = new List<DNSAnswerSection>();
        }

        public ICollection<DNSQuestionSection> Questions { get; set; }
        public ICollection<DNSAnswerSection> Answers { get; set; }

        public ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            var headerPayload = Header.Serialize();
            result.AddRange(headerPayload);
            var mappingOffsetDomainNames = new Dictionary<string, short>();
            short currentOffset = (short)headerPayload.Count();
            foreach (var question in Questions)
            {
                mappingOffsetDomainNames.Add(question.Label, currentOffset);
                var questionPayload = question.Serialize();
                result.AddRange(questionPayload);
                currentOffset += (short)questionPayload.Count();
            }

            foreach(var answer in Answers)
            {
                var offset = (short)(mappingOffsetDomainNames[answer.Label] | 0xc000);
                result.AddRange(offset.ToBytes());
                result.AddRange(answer.ResourceRecord.ResourceType.ToBytes());
                result.AddRange(answer.ResourceRecord.ResourceClass.ToBytes());
                result.AddRange(answer.ResourceRecord.Ttl.Value.ToBytes());
                result.AddRange(new List<byte> { 0x00, 0x04, 0xd8, 0x3a, 0x6e, 0x01 });
            }

            return result;
        }
    }
}
