using System;
using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models
{
    public abstract class Transaction
    {
        protected Transaction(Account account, decimal money)
        {
            if (money < 0)
                throw new BanksException("Money can't be less than zero");

            Account = account ?? throw new BanksException("Account is null");
            Money = money;
            Id = Guid.NewGuid();
        }

        public Account Account { get; }
        public decimal Money { get; }
        public Guid Id { get; }
        public bool CanceledTransaction { get; protected set; } = false;
        public bool CompletedTransaction { get; protected set; } = false;

        public abstract void TransactionWithAccount();
        public abstract void Cancel();
    }
}