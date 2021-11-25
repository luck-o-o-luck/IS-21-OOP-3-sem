using System;
using Banks.Models;
using Banks.Models.Banks;
using Banks.Tools;

namespace Banks.Commands
{
    public class CreateClientCommand : ICommand
    {
        private string _fullName;
        private string _passport;
        private Bank _bank;
        public CreateClientCommand(string fullName, string passport, string bankName)
        {
            var centralBank = CentralBank.GetInstance();
            var bank = centralBank.FindBank(bankName);

            _fullName = fullName;
            _passport = passport;
            _bank = bank ?? throw new BanksException("Bank doesn't exist");
        }

        public void Execute()
        {
            var centralBank = CentralBank.GetInstance();
            var client = centralBank.CreateClient(_fullName, _passport, _bank);
            Console.WriteLine("Do you want set address? y/n");
            if (Console.ReadLine() == "y")
            {
                client.SetAddress(new Address(
                    Console.ReadLine(),
                    Convert.ToInt16(Console.ReadLine()),
                    Convert.ToInt16(Console.ReadLine())));
            }

            Console.WriteLine("Do you want set number phone? For example, 89213674849. y/n");
            if (Console.ReadLine() == "y")
            {
                client.SetNumberPhone(Console.ReadLine());
            }

            Console.WriteLine("Do you want have subscribe? y/n");
            if (Console.ReadLine() == "y")
            {
                _bank.Attach(client);
            }

            _bank.AddClientForBank(client);

            Console.WriteLine($"Client {client.ToString()} created");
        }
    }
}
