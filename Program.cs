using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace DasBootCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();
            
            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();
            
            Blockchain dasBootCoin = new Blockchain(2, 100);
            
            Console.WriteLine("Start the Miner.");
            dasBootCoin.MinePendingTransactions(wallet1);

            Console.WriteLine("\nBalance of wallet1 is $" + dasBootCoin.GetBalanceOfWallet(wallet1));

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            dasBootCoin.AddPendingTransaction(tx1);
            
            Console.WriteLine("Start the Miner.");
            dasBootCoin.MinePendingTransactions(wallet2);
            
            Console.WriteLine("\nBalance of wallet1 is $" + dasBootCoin.GetBalanceOfWallet(wallet1));
            Console.WriteLine("\nBalance of wallet2 is $" + dasBootCoin.GetBalanceOfWallet(wallet2));


            string blockJSON = JsonConvert.SerializeObject(dasBootCoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            // dasBootCoin.GetLatestBlock().PreviousHash = "12345";
            
            if (dasBootCoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is valid!!!!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid.");
            }
        }
    }
}
