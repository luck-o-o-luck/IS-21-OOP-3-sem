using System;
using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models
{
    public class Transaction<TAccount>
        where TAccount : Account
    {
        public Transaction(TAccount account, decimal money)
        {
            if (money < 0)
                throw new BanksException("Money can't be less than zero");

            Account = account ?? throw new BanksException("Account is null");
            Money = money;
            Id = Guid.NewGuid();
        }

        public TAccount Account { get; }
        public decimal Money { get; }
        public Guid Id { get; }
        public bool CanceledTransaction { get; private set; } = false;
        public bool CompletedTransaction { get; private set; } = false;

        public Transaction<TAccount> RemittanceToAccount(Account account)
        {
            Account.WithdrawMoney(Money);
            account.DepositMoney(Money);

            CompletedTransaction = true;

            return this;
        }

        public Transaction<TAccount> WithdrawMoneyAccount()
        {
            Account.WithdrawMoney(Money);
            CompletedTransaction = true;

            return this;
        }

        public Transaction<TAccount> DepositMoneyAccount()
        {
            Account.DepositMoney(Money);
            CompletedTransaction = true;

            return this;
        }

        public void Cancel()
        {
            if (!CompletedTransaction)
                throw new BanksException("Transaction isn't complete");
            if (CanceledTransaction)
                throw new BanksException("Transaction already canceled");

            DepositMoneyAccount();
            CanceledTransaction = true;
        }
    }
}