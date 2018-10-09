using System;
using System.Collections.Generic;
using System.Linq;
using Sharpchain.Helpers;
using Sharpchain.Model;

namespace Sharpchain
{
    public class Blockchain
    {
        #region Public Fields
        public int Difficulty { get; set; }

        public List<Block> GetBlocks => _blockchain;
        #endregion

        #region Private Fields
        private readonly List<Block> _blockchain = new List<Block> { };
  
        private Block LatestBlock => _blockchain.Last();
        #endregion

        public Blockchain()
        {
            if (Difficulty <= 0)
                Difficulty = 3;

            _blockchain.Add(Genesis());
        }

        public Block Mine(string data)
        {
            Block newBlock = GenerateNextBlock(data);

            if (this.IsValidNextBlock(this.LatestBlock, newBlock))
                _blockchain.Add(newBlock);
            else
                Console.WriteLine($"Error : Invalid block : {newBlock}");

            return LatestBlock;
        }

        private Block GenerateNextBlock(string data)
        {
            Int64 nextIndex = this.LatestBlock.Index + 1;
            string prevHash = this.LatestBlock.Hash;
            DateTime timeStamp = DateTime.Now;
            int nonce = 0;

            string nextHash = CalculateHash(nextIndex, prevHash, timeStamp, data, nonce);
            
            while (!this.IsValidHashDifficulty(nextHash))
            {
                nonce++;
                timeStamp = DateTime.Now;

                nextHash = CalculateHash(nextIndex, prevHash, timeStamp, data, nonce);

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

        private string CalculateHash(Int64 index, string prevHash, DateTime timeStamp, string data, int nonce)
        {
            string blockFootprint = $"{index}{prevHash}{timeStamp}{data}{nonce}";
            return blockFootprint.ToSHA256HashString();
        }

        private string CalculateBlockHash(Block block)
        {
            string blockFootprint = $"{block.Index}{block.PrevHash}{block.TimeStamp}{block.Data}{block.Nonce}";
            return blockFootprint.ToSHA256HashString();
        }

        private bool IsValidNextBlock(Block prevBlock, Block nextBlock)
        {
            string nextBlockHash = CalculateBlockHash(nextBlock);

            if (prevBlock.Index + 1 != nextBlock.Index)
                return false;


            if (prevBlock.Hash != nextBlock.PrevHash)
                return false;


            if (nextBlockHash != nextBlock.Hash)
                return false;


            if (!this.IsValidHashDifficulty(nextBlockHash))
                return false;

            return true;
        }

        private bool IsValidHashDifficulty(string hash)
        {
            string difficultyZeroString = new string('0', Difficulty);
            return hash.StartsWith(difficultyZeroString);
        }

        private Block Genesis()
        {
            Int64 index = 0;
            string prevHash = "0";
            DateTime timeStamp = DateTime.Now;
            string data = "Sharpen genesis block !";
            int nonce = 0;

            string nextHash = CalculateHash(index, prevHash, timeStamp, data, nonce);

            while (!this.IsValidHashDifficulty(nextHash))
            {
                nonce++;
                timeStamp = DateTime.Now;

                nextHash = CalculateHash(index, prevHash, timeStamp, data, nonce);
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