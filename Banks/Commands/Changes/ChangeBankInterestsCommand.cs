using System;
using Banks.Models.Banks;
using Banks.Models.Clients;

namespace Banks.Commands
{
    public class ChangeBankInterestsCommand : ICommand
    {
        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();

            Console.WriteLine("Enter name of bank");

            var bank = centralBank.FindBank(Console.ReadLine());

            Console.WriteLine("Do you want change debit percents? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeDebitPercent(decimal.Parse(Console.ReadLine()), bank);
            }

            Console.WriteLine("Do you want change transaction limit? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeTransactionLimit(decimal.Parse(Console.ReadLine()), bank);
            }

            Console.WriteLine("Do you want change minimal deposit percents? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeMinimalDepositPercent(decimal.Parse(Console.ReadLine()), bank);
            }

            Console.WriteLine("Do you want change middle deposit percents? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeMiddleDepositPercent(decimal.Parse(Console.ReadLine()), bank);
            }

            Console.WriteLine("Do you want change maximum deposit percents? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeMaximumDepositPercent(decimal.Parse(Console.ReadLine()), bank);
            }

            Console.WriteLine("Do you want change credit commission? y/n");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("Enter a new value");
                centralBank.ChangeCreditCommission(decimal.Parse(Console.ReadLine()), bank);
            }

            foreach (Client client in bank.Clients)
            {
                foreach (string update in client.Updates)
                    Console.WriteLine($"{update}\n");
            }

            foreach (Client client in bank.Clients)
            {
                Console.WriteLine($"Client {client.FullName} has subscribe, so:\n");
                foreach (string update in client.Updates)
                    Console.WriteLine(update);
            }
        }
    }
}