using System;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(decimal balance, Client client, Bank bank)
            : base(balance, client, bank)
        {
            InterestBalance = Bank.BankInterest.DebitPercent;
            AmountInterestPayments = 0;
        }

        public decimal InterestBalance { get; }
        public decimal AmountInterestPayments { get; private set; }

        public void InterestPayments()
        {
            AmountInterestPayments += Balance * InterestBalance / 100;
        }

        public override void DepositMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");

            Balance += money;
        }

        public override void WithdrawMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");

            if (Balance < money)
                throw new BanksException("Can't withdraw money");

            if (AccountOwner.IsDoubtfulClient)
            {
                if (money > Bank.BankInterest.TransactionLimit)
                    throw new BanksException("You can't withdraw money more than limit");
            }

            Balance -= money;
        }

        public override void AddCommission(int days)
        {
            Balance += AmountInterestPayments;
            AmountInterestPayments = 0;
        }

        public override void UpdateCommissions(int days)
        {
            if (days < 0)
                throw new BanksException("Days can't be less than zero");

            for (int i = 0; i < days; i++)
                InterestPayments();
        }
    }
}