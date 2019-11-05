namespace DnsServer.Domains
{
    public class ResourceClasses : BaseDomainEnum
    {
        public ResourceClasses(short value) : base(value) { }

        /// <summary>
        /// The internet.
        /// </summary>
        public static ResourceClasses IN = new ResourceClasses(1);
        /// <summary>
        /// The CSNET class.
        /// </summary>
        public static ResourceClasses CS = new ResourceClasses(2);
        /// <summary>
        /// The CHAOS class.
        /// </summary>
        public static ResourceClasses CH = new ResourceClasses(3);
        /// <summary>
        /// Hesiod.
        /// </summary>
        public static ResourceClasses HS = new ResourceClasses(4);
    }
}
