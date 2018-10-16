using System;
using Sharpchain.Blockchain;
using Sharpchain.Helpers;

namespace Sharpchain.Model
{
    public class Block : IBlock
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

        public IBlock Genesis(int difficulty)
        {
            Int64 index = 0;
            string prevHash = "0";
            DateTime timeStamp = DateTime.Now;
            string data = "Sharpen genesis block !";
            int nonce = 0;

            string nextHash = new Block
            {
                Index = index,
                PrevHash = prevHash,
                TimeStamp = timeStamp,
                Data = data,
                Nonce = nonce
            }.ComputeHashString();

            while (! nextHash.IsValidHashDifficulty(difficulty))
            {
                nonce++;
                timeStamp = DateTime.Now;

                nextHash = new Block
                {
                    Index = index,
                    PrevHash = prevHash,
                    TimeStamp = timeStamp,
                    Data = data,
                    Nonce = nonce
                }.ComputeHashString();
            }

            return new Block
            {
                Index = index,
                PrevHash = prevHash,
                TimeStamp = timeStamp,
                Data = data,
                Hash = nextHash,
                Nonce = nonce
            };
        }
    }
}