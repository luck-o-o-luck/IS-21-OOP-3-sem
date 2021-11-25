using System;
using Banks.Models;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Commands
{
    public class CreateClientCommand : ICommand
    {
        private const int _maxCountOfNumber = 11;
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
                string street = Console.ReadLine();
                int numberHouse = Convert.ToInt16(Console.ReadLine());
                int numberApartment = Convert.ToInt16(Console.ReadLine());

                if (string.IsNullOrEmpty(street))
                {
                    Console.WriteLine("Enter your street");
                    street = Console.ReadLine();
                }

                if (numberHouse < 0)
                {
                    Console.WriteLine("Number house can't be less than zero");
                    numberHouse = Convert.ToInt16(Console.ReadLine());
                }

                if (numberApartment < 0)
                {
                    Console.WriteLine("Number apartment can't be less than zero");
                    numberApartment = Convert.ToInt16(Console.ReadLine());
                }

                client.SetAddress(new Address(street, numberHouse, numberApartment));
            }

            Console.WriteLine("Do you want set number phone? For example, 89213674849. y/n");
            if (Console.ReadLine() == "y")
            {
                string number = Console.ReadLine();

                if (number.Length != _maxCountOfNumber)
                {
                    Console.WriteLine("You entered your phone number incorrectly. Enter, for example, 89213674849");
                    number = Console.ReadLine();
                }

                client.SetNumberPhone(number);
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
