using System;
using Sharpchain.Model;

namespace Sharpchain
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "# Sharpchain by Felony #";

            int numberOfBlocksToMine = 3;

            var blockchain = new Blockchain{Difficulty = 4};

            for (int i = 1; i <= numberOfBlocksToMine; i++)
            {
                Console.WriteLine($"Mining new block - Difficulty : {blockchain.Difficulty}");

                Block minedBlock = blockchain.Mine($"Random data in loop {i}");
            }

            Console.WriteLine();
            foreach (Block block in blockchain.GetBlocks)
            {
                Console.WriteLine(block);
            }

            Console.Read();
        }
    }
}