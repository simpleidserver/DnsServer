using DnsServer.Domains;

namespace DnsServer.Messages
{
    public class QuestionClasses : ResourceClasses
    {
        /// <summary>
        /// Any classes.
        /// </summary>
        public static ResourceClasses START = new ResourceClasses(255);

        public QuestionClasses(uint value) : base(value) { }
    }
}
