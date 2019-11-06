using DnsServer.Domains;
using DnsServer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DnsServer.Messages
{
    public class DNSWriterBufferContext
    {
        public DNSWriterBufferContext(List<byte> buffer)
        {
            Buffer = buffer;
            ZoneLabels = new List<ICollection<DNSZoneLabel>>();
        }

        public List<byte> Buffer { get; set; }
        public List<ICollection<DNSZoneLabel>> ZoneLabels { get; set; }

        public void WriteUInt(uint i)
        {
            Buffer.AddRange(i.ToBytes());
        }

        public void WriteIPV4(string str)
        {
            var ip = IPAddress.Parse(str);
            var payload = ip.GetAddressBytes();
            Buffer.AddRange(payload);
        }

        public void WriteInt(int i)
        {
            Buffer.AddRange(i.ToBytes());
        }

        public void WriteFlag(DNSHeaderFlags flag)
        {
            Buffer.AddRange(flag.ToBytes());
        }

        public void WriteEnum(BaseDomainEnum e)
        {
            Buffer.AddRange(e.ToBytes());
        }

        public void WriteString(string str)
        {
            Buffer.AddRange(str.ToBytes());
        }

        public void WriteIPV6(string str)
        {
            var ip = IPAddress.Parse(str);
            var payload = ip.GetAddressBytes();
            Buffer.AddRange(payload);
        }

        public void WriteLabel(string str)
        {
            var kvp = GetLabel(str, Buffer.Count);
            Buffer.AddRange(kvp.Key);
            ZoneLabels.Add(kvp.Value);
        }

        public KeyValuePair<List<byte>, List<DNSZoneLabel>> GetLabel(string str, int currentOffset)
        {
            var lst = new List<DNSZoneLabel>();
            var result = new List<byte>();
            var labels = str.Split('.');
            int nb = 0;
            foreach (var label in labels)
            {
                var zoneLabel = ZoneLabels.SelectMany(s => s).FirstOrDefault(s => s.Label == label);
                if (zoneLabel != null)
                {
                    var offset = zoneLabel.CurrentOffset | 0xc000;
                    currentOffset += 2;
                    result.AddRange(offset.ToBytes());
                    return new KeyValuePair<List<byte>, List<DNSZoneLabel>>(result, lst);
                }

                lst.Add(new DNSZoneLabel(label, (uint)currentOffset));
                var payload = label.ToBytes();
                currentOffset += payload.Count();
                result.AddRange(payload);

                if (nb == labels.Count() - 1)
                {
                    result.Add(0x00);
                    currentOffset++;
                }

                nb++;
            }

            return new KeyValuePair<List<byte>, List<DNSZoneLabel>>(result, lst);
        }
    }
}
