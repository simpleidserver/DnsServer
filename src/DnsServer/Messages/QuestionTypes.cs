using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class QuestionTypes : ResourceTypes
    {
        public QuestionTypes(uint value) : base(value) { }
        
        /// <summary>
        /// A request for a transfer of an entire zone.
        /// </summary>
        public static QuestionTypes AXFR = new QuestionTypes(252);
        /// <summary>
        /// A request for mailbox-related records (MB, MG or MR).
        /// </summary>
        public static QuestionTypes MAILB = new QuestionTypes(253);
        /// <summary>
        /// A request for mail agent RRs.
        /// </summary>
        public static QuestionTypes MAILA = new QuestionTypes(254);
        /// <summary>
        /// A request for all records.
        /// </summary>
        public static QuestionTypes STAR = new QuestionTypes(255);
    }
}
