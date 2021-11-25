using System;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class CancelTransactionCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();
            Console.WriteLine("Enter transactions id");
            centralBank.CancelTransaction(Console.ReadLine());
            Console.WriteLine("Canceled transaction passed successfully");
        }
    }
}