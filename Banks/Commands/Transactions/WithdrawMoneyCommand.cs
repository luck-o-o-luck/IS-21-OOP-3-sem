using System;
using System.Linq;
using Banks.Models.Banks;
using Banks.Tools;

namespace Banks.Commands
{
    public class WithdrawMoneyCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();

            Console.WriteLine("Enter name of bank");

            var bank = centralBank.FindBank(Console.ReadLine());
            if (bank is null)
                throw new BanksException("Bank doesn't exists");

            Console.WriteLine("Enter Id Account");

            var account = bank.Accounts.SingleOrDefault(x => x.Id.ToString() == Console.ReadLine());
            if (account is null)
                throw new BanksException("Account doesn't exists");

            Console.WriteLine("Enter money for transaction");

            var transaction = centralBank.CreateTransactionWithdrawMoney(account, bank, Convert.ToDecimal(Console.ReadLine()));

            Console.WriteLine("Withdraw money passed successfully");
        }
    }
}