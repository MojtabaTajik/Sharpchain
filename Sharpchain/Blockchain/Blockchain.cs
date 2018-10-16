using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharpchain.Helpers;
using Sharpchain.Model;

namespace Sharpchain.Blockchain
{
    public class Blockchain : IEnumerable<IBlock>
    {
        #region Public Fields

        public int Difficulty { get; set; }

        public List<IBlock> Items { get; set; } = new List<IBlock> { };

        public IBlock this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }
        #endregion

        #region Private Fields

        private Block LatestBlock => (Block) Items.Last();

        #endregion

        public Blockchain(int difficulty = 3)
        {
            Difficulty = difficulty;

            Items.Add(new Block().Genesis(difficulty));
        }

        public IBlock Mine(string data)
        {
            IBlock newBlock = GenerateNextBlock(data);

            if (this.LatestBlock.IsValidNextBlock(newBlock, Difficulty))
                Items.Add(newBlock);
            else
                throw new Exception($"Error : Invalid block : {newBlock}");

            return LatestBlock;
        }

        private IBlock GenerateNextBlock(string data)
        {
            Int64 nextIndex = this.LatestBlock.Index + 1;
            string prevHash = this.LatestBlock.Hash;
            DateTime timeStamp = DateTime.Now;
            int nonce = 0;

            string nextHash = new Block
            {
                Index = nextIndex,
                PrevHash = prevHash,
                TimeStamp = timeStamp,
                Data = data,
                Nonce = nonce
            }.ComputeHashString();
                
            while (! nextHash.IsValidHashDifficulty(Difficulty))
            {
                nonce++;
                timeStamp = DateTime.Now;

                nextHash = new Block
                {
                    Index = nextIndex,
                    PrevHash = prevHash,
                    TimeStamp = timeStamp,
                    Data = data,
                    Nonce = nonce
                }.ComputeHashString();

                Console.Write($"\r{nextHash}, {nonce}");
            }
            Console.WriteLine(Environment.NewLine);

            return new Block
            {
                Index = nextIndex,
                PrevHash = prevHash,
                TimeStamp = timeStamp,
                Data = data,
                Hash = nextHash,
                Nonce = nonce
            };
        }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}