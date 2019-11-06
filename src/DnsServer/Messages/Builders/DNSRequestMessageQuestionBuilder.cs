using DnsServer.Domains;

namespace DnsServer.Messages.Builders
{
    public class DNSRequestMessageQuestionBuilder
    {
        private readonly DNSRequestMessage _dnsRequestMessage;

        public DNSRequestMessageQuestionBuilder(DNSRequestMessage dnsRequestMessage)
        {
            _dnsRequestMessage = dnsRequestMessage;
        }

        public DNSRequestMessageQuestionBuilder AddQuestion(string label, ResourceClasses resourceClass, ResourceTypes resourceType)
        {
            _dnsRequestMessage.Questions.Add(new DNSQuestionSection
            {
                Label = label,
                QClass = resourceClass,
                QType = resourceType
            });
            _dnsRequestMessage.Header.QdCount++;
            return this;
        }

        public DNSRequestMessage Build()
        {
            return _dnsRequestMessage;
        }
    }
}
