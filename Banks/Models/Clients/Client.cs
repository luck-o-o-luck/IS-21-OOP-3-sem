using System.Collections.Generic;
using Backups.Tools;
using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models.Clients
{
    public class Client
    {
        private const int _maxCountNumbersOfNumberPhone = 11;
        private List<Account> _accounts;

        public Client(string fullName, string passport, string numberPhone, Address address)
        {
            if (!string.IsNullOrEmpty(numberPhone))
            {
                if (numberPhone.Length != _maxCountNumbersOfNumberPhone)
                    throw new BanksException("Invalid phone number");
            }

            if (string.IsNullOrEmpty(fullName))
                throw new BanksException("Name doesn't exist");
            if (string.IsNullOrEmpty(passport))
                throw new BanksException("Passport doesn't exist");

            _accounts = new List<Account>();
            FullName = fullName;
            Passport = passport;
            NumberPhone = numberPhone;
            Address = address;
        }

        public string FullName { get; }
        public string Passport { get; }
        public string NumberPhone { get; private set; }
        public Address Address { get; private set; }
        public bool IsDoubtfulClient => Address == null || string.IsNullOrEmpty(NumberPhone);

        public void SetNumberPhone(string numberPhone)
        {
            NumberPhone = numberPhone;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }
    }
}