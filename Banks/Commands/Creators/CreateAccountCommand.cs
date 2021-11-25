using System;
using Banks.Models.Accounts;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class CreateAccountCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();
            Console.WriteLine("Enter passport bank and client");
            var bank = centralBank.FindBank(Console.ReadLine());
            var client = centralBank.FindClient(Console.ReadLine(), bank);

            Console.WriteLine("Do you want create deposit account? y/n");
            if (Console.ReadLine() == "y")
            {
               Console.WriteLine("Enter date");
               string date = Console.ReadLine();
               DateTime dateTime = new DateTime(
                   Convert.ToInt32(date.Substring(4, 4)),
                   Convert.ToInt32(date.Substring(2, 2)),
                   Convert.ToInt32(date.Substring(0, 2)));
               Console.WriteLine("Enter first money");
               var account = new DepositAccount(Convert.ToDecimal(Console.ReadLine()), client, dateTime, bank);
               bank.AddAccountForBank(account);
               client.AddAccountForClient(account);
               Console.WriteLine($"Account {account.ToString()} created");
            }

            Console.WriteLine("Do you want create debit account? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter first money");
                var account = new DebitAccount(Convert.ToDecimal(Console.ReadLine()), client, bank);
                bank.AddAccountForBank(account);
                client.AddAccountForClient(account);
                Console.WriteLine($"Account {account.ToString()} created");
            }

            Console.WriteLine("Do you want create credit account? y/n");
            if (Console.ReadLine() == "y")
            {
                var account = new CreditAccount(client, bank);
                bank.AddAccountForBank(account);
                client.AddAccountForClient(account);
                Console.WriteLine($"Account {account.ToString()} created");
            }
        }
    }
}