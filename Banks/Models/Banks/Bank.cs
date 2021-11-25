using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;
using Banks.Models.Accounts;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Banks
{
    public class Bank : ISubject
    {
        private List<Account> _accounts;
        private List<Client> _clients;

        public Bank(string name, BankInterest bankInterest)
        {
            if (string.IsNullOrEmpty(name))
                throw new BanksException("Name is null");

            _accounts = new List<Account>();
            _clients = new List<Client>();
            Name = name;
            BankInterest = bankInterest ?? throw new BanksException("Interests is null");
            Id = Guid.NewGuid();
        }

        public string Name { get; }
        public Guid Id { get; }
        public BankInterest BankInterest { get; private set; }
        public IReadOnlyList<Account> Accounts => _accounts;
        public IReadOnlyList<Client> Clients => _clients;

        public void AddAccountForBank(Account account)
        {
            if (account is null)
                throw new BanksException("Account is not created");

            _accounts.Add(account);
        }

        public void AddClientForBank(Client client)
        {
            if (client is null)
                throw new BanksException("Client doesn't exist");

            _clients.Add(client);
        }

        public void ChangeBankInterest(BankInterest newInterest) => BankInterest = newInterest;

        public void Attach(Client client)
        {
            if (client is null)
                throw new BanksException("Client is null");

            client.ChangeSubscribe(true);
        }

        public void Detach(Client client)
        {
            if (client is null)
                throw new BanksException("Client is null");

            client.ChangeSubscribe(false);
        }

        public void Notify(string updateValue, decimal newValue)
        {
            var selectedClients = _clients.Where(client => client.IsSubscribed);

            foreach (Client client in selectedClients)
            {
                var update = $"Bank interests updates: {updateValue} is {newValue}";
                client.Update(update);
            }
        }

        public override string ToString() => $"{Name} {Id}";
    }
}