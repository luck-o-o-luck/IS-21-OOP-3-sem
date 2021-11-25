using System.Collections.Generic;
using Banks.Interfaces;
using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models.Clients
{
    public class Client : IObserver
    {
        private const int _maxCountNumbersOfNumberPhone = 11;
        private List<Account> _accounts;
        private List<string> _updates;

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
            _updates = new List<string>();
            FullName = fullName;
            Passport = passport;
            NumberPhone = numberPhone;
            Address = address;
            IsSubscribed = false;
        }

        public string FullName { get; }
        public string Passport { get; }
        public string NumberPhone { get; private set; }
        public Address Address { get; private set; }
        public bool IsDoubtfulClient => Address == null || string.IsNullOrEmpty(NumberPhone);
        public bool IsSubscribed { get; private set; }
        public IReadOnlyList<string> Updates => _updates;

        public void SetNumberPhone(string numberPhone)
        {
            if (string.IsNullOrEmpty(numberPhone))
                throw new BanksException("Phone number is null");
            if (numberPhone.Length != _maxCountNumbersOfNumberPhone)
                throw new BanksException("Invalid phone number");

            NumberPhone = numberPhone;
        }

        public void SetAddress(Address address)
        {
            Address = address ?? throw new BanksException("Address is null");
        }

        public void AddAccountForClient(Account account)
        {
            if (account is null)
                throw new BanksException("Account is null");

            _accounts.Add(account);
        }

        public void Update(string update)
        {
            if (string.IsNullOrEmpty(update))
                throw new BanksException("Update is null");

            _updates.Add(update);
        }

        public void ClearAfterUpdate() => _updates.Clear();

        public void ChangeSubscribe(bool newSubscribe) => IsSubscribed = newSubscribe;
        public override string ToString() => FullName;
    }
}
