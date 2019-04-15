using System;
using System.Collections.Generic;

namespace Blockchain
{
    public class Blockchain
    {
        public int Reward { set; get; }
        public int Difficulty { set; get; } = 2;
        public IList<Block> Chain { set; get; }
        public IList<Transaction> PendingTransactions = new List<Transaction>();


        public Blockchain(int reward = 1)
        {
            Reward=reward;
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public void AddGenesisBlock()
        {
            Block genesisBlock= new Block(DateTime.Now, null, new List<Transaction>());
            genesisBlock.Mine(this.Difficulty);
            Chain.Add(genesisBlock);
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            AddBlock(block);

            PendingTransactions = new List<Transaction>();
            int newId = PendingTransactions[PendingTransactions.Count - 1].Id + 1;
            CreateTransaction(new Transaction(newId, null, minerAddress, Reward));
        }

        public override string ToString() {
            String result = "";
            foreach(Block block in Chain)
            {
                result += block.ToString();
            }
            return result;
        }
    }
}
