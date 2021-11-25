using System;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class TryUpdatePaymentsCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();
            centralBank.TryUpdatePayments();

            Console.WriteLine("Update payments passed successfully");
        }
    }
}