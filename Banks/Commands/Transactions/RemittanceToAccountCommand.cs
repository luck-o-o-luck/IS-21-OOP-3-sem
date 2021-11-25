using System;
using System.Linq;
using Banks.Models.Banks;
using Banks.Tools;

namespace Banks.Commands
{
    public class RemittanceToAccountCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();

            Console.WriteLine("Enter name of bank");

            var bank = centralBank.FindBank(Console.ReadLine());
            if (bank is null)
                throw new BanksException("Bank doesn't exists");

            Console.WriteLine("Enter Id Account for withdraw");

            var withdrawAccount = bank.Accounts.SingleOrDefault(x => x.Id.ToString() == Console.ReadLine());
            if (withdrawAccount is null)
                throw new BanksException("Account doesn't exists");

            Console.WriteLine("Enter Id Account for deposit");

            var depositAccount = bank.Accounts.SingleOrDefault(x => x.Id.ToString() == Console.ReadLine());
            if (depositAccount is null)
                throw new BanksException("Account doesn't exists");

            Console.WriteLine("Enter money for transaction");

            var transaction = centralBank.CreateTransaction(withdrawAccount, bank, Convert.ToDecimal(Console.ReadLine()));
            transaction.RemittanceToAccount(depositAccount);

            Console.WriteLine("Remmitance passed successfully");
        }
    }
}