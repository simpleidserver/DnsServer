using DnsServer.Extensions;
using System.Collections.Generic;

namespace DnsServer.Messages
{
    public class AXFRHeader
    {
        public short Id { get; set; }
        
        public ICollection<byte> Serialize()
        {
            var result = new List<byte>();
            result.AddRange(Id.ToBytes());
            result.Add(0x00); // Query.
            result.AddRange(new List<byte> { 0x00, 0x00, 0x00, 0x00 }); // Standard query.
            result.Add(0x00); // AA
            result.Add(0x00); // TC
            result.Add(0x00); // RD
            result.Add(0x00); // RA
            result.Add(0x00); // Z
            result.Add(0x00); // AD
            result.Add(0x00); // CD
            result.AddRange(new List<byte> { 0x00, 0x00, 0x00, 0x00 }); // RCode  : (No error)
            result.Add(0x01); // Number of entries in Question section.
            result.Add(0x00); // Number of entries in Answer section.
            result.Add(0x00); // Number of entries in Authority section.
            result.Add(0x00); // Number of entries in Additional section.
            return result;
        }
    }
}
