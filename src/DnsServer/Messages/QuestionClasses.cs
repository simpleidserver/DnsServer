using DnsServer.Domains;
using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Messages
{
    public class QuestionClasses : ResourceClasses
    {
        /// <summary>
        /// Any classes.
        /// </summary>
        public static QuestionClasses START = new QuestionClasses(255);

        public QuestionClasses(short value) : base(value) { }
    }
}
