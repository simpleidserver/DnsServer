using DnsServer.Messages.Serializers;
using System.Collections.Generic;
using System.Linq;

namespace DnsServer.Messages
{
    public class DNSResponseMessage : DNSMessage
    {
        public DNSResponseMessage()
        {
            Questions = new List<DNSQuestionSection>();
            Answers = new List<DNSResourceRecord>();
            AuthoritativeNamespaceServers = new List<DNSResourceRecord>();
            AdditionalRecords = new List<DNSResourceRecord>();
        }

        public ICollection<DNSQuestionSection> Questions { get; set; }
        public ICollection<DNSResourceRecord> Answers { get; set; }
        public ICollection<DNSResourceRecord> AuthoritativeNamespaceServers { get; set; }
        public ICollection<DNSResourceRecord> AdditionalRecords { get; set; }

        public static DNSResponseMessage Extract(byte[] buffer)
        {
            var dnsBufferContext = new DNSReadBufferContext(buffer);
            var result = new DNSResponseMessage
            {
                Header = DNSHeader.Extract(dnsBufferContext)
            };

            for (var i = 0; i < result.Header.QdCount; i ++)
            {
                var question = DNSQuestionSection.Extract(dnsBufferContext);
                result.Questions.Add(question);
            }

            for(var i = 0; i < result.Header.AnCount; i++)
            {
                result.Answers.Add(ResourceRecordSerializer.Extract(dnsBufferContext));
            }

            for(var i = 0; i < result.Header.NsCount; i++)
            {
                result.AuthoritativeNamespaceServers.Add(ResourceRecordSerializer.Extract(dnsBufferContext));
            }

            for(var i = 0; i < result.Header.ArCount; i++)
            {
                result.AdditionalRecords.Add(ResourceRecordSerializer.Extract(dnsBufferContext));
            }
            
            return result;
        }

        public ICollection<byte> Serialize()
        {
            var context = new DNSWriterBufferContext(new List<byte>());
            Header.Serialize(context);
            foreach (var question in Questions)
            {
                question.Serialize(context);
            }

            foreach(var answer in Answers)
            {
                ResourceRecordSerializer.Serialize(context, answer);
            }

            foreach (var authoritativeNamespaceServer in AuthoritativeNamespaceServers)
            {
                ResourceRecordSerializer.Serialize(context, authoritativeNamespaceServer);
            }

            foreach (var additionalRecords in AdditionalRecords)
            {
                ResourceRecordSerializer.Serialize(context, additionalRecords);
            }

            return context.Buffer;
        }
    }
}
