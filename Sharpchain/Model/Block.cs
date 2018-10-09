using System;

namespace Sharpchain.Model
{
    public class Block
    {
        public Int64 Index { get; set; }
        public string PrevHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }
        public Int64 Nonce { get; set; }

        public override string ToString()
        {
            return $"{Index}:{TimeStamp} ,{PrevHash, 64}, {Data}, {Hash, 64}, {Nonce}";
        }
    }
}