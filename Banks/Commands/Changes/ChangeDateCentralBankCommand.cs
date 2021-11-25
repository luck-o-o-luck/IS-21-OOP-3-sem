using System;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class ChangeDateCentralBankCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Enter amount days which you want to add");
            int days = int.Parse(Console.ReadLine());
            var centralBank = CentralBank.GetInstance();
            centralBank.RewindTimeDays(days);
            centralBank.TryUpdatePayments();
        }
    }
}