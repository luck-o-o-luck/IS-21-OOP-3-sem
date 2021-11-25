using System;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Accounts
{
    public abstract class Account
    {
        protected Account(decimal balance, Client client, Bank bank)
        {
            if (balance < 0)
                throw new BanksException("Invalid balance");

            Id = Guid.NewGuid();
            Balance = balance;
            AccountOwner = client ?? throw new BanksException("Can't created account without client");
            Bank = bank ?? throw new BanksException("Can't created account without bank");
        }

        public Guid Id { get; }
        public decimal Balance { get; internal set; } = 0;
        public Client AccountOwner { get; }
        public Bank Bank { get; }

        public abstract void WithdrawMoney(decimal money);
        public abstract void DepositMoney(decimal money);
        public abstract void AddCommission(int days);
        public abstract void UpdateCommissions(int days);
        public override string ToString() => $"{Id}";
    }
}