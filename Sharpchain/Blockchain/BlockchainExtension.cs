using Sharpchain.Helpers;
using Sharpchain.Model;

namespace Sharpchain.Blockchain
{
    public static class BlockchainExtension
    {
        public static byte[] ComputeHash(this IBlock block)
        {
            string blockFootprint = $"{block.Index}{block.PrevHash}{block.TimeStamp}{block.Data}{block.Nonce}";
            return blockFootprint.ToSha256Hash();
        }

        public static string ComputeHashString(this IBlock block)
        {
            string blockFootprint = $"{block.Index}{block.PrevHash}{block.TimeStamp}{block.Data}{block.Nonce}";
            return blockFootprint.ToSha256HashString();
        }

        public static bool IsValidNextBlock(this IBlock prevBlock, IBlock nextBlock, int difficulty)
        {
            string nextBlockHash = nextBlock.ComputeHashString();

            if (prevBlock.Index + 1 != nextBlock.Index)
                return false;


            if (prevBlock.Hash != nextBlock.PrevHash)
                return false;


            if (nextBlockHash != nextBlock.Hash)
                return false;


            if (! nextBlockHash.IsValidHashDifficulty(difficulty))
                return false;

            return true;
        }
    }
}