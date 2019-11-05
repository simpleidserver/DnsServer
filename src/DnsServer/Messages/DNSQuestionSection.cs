using DnsServer.Extensions;
using System.Collections.Generic;
using System.Text;

namespace DnsServer.Messages
{
    public class DNSQuestionSection
    {
        /// <summary>
        /// Domain name.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// A two octet code which specifies the type of the query.
        /// </summary>
        public QuestionTypes QType { get; set; }
        /// <summary>
        /// A two octet code that specifies the class of the query.
        /// </summary>
        public QuestionClasses QClass { get; set; }

        public static DNSQuestionSection Extract(Queue<byte> buffer)
        {
            var str = Encoding.ASCII.GetString(buffer.ToArray());
            var result = new DNSQuestionSection
            {
                Label = buffer.GetLabel(),
                QType = new QuestionTypes(buffer.GetShort()),
                QClass = new QuestionClasses(buffer.GetShort())
            };
            return result;
        }

        public ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            result.AddRange(Label.ConvertLabelToBytes());
            result.AddRange(QType.ToBytes());
            result.AddRange(QClass.ToBytes());
            return result;
        }
    }
}
