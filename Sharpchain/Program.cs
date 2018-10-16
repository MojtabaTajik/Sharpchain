using System;
using Sharpchain.Model;

namespace Sharpchain
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "# Sharpchain by Felony #";

            const int difficulty = 4;
            int numberOfBlocksToMine = 3;

            var blockchain = new Blockchain.Blockchain(difficulty);

            for (int i = 1; i <= numberOfBlocksToMine; i++)
            {
                try
                {
                    Console.WriteLine($"Mining new block - Difficulty : {blockchain.Difficulty}");

                    IBlock minedBlock = blockchain.Mine($"Random data in loop {i}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine();
            foreach (IBlock block in blockchain)
            {
                Console.WriteLine(block);
            }

            Console.Read();
        }
    }
}