using System;
using System.Collections;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal balance, Client client, DateTime date, Bank bank)
            : base(balance, client, bank)
        {
            ReferenceDate = date;
            AmountInterestPayments = 0;
            UpdatePercents();
        }

        public decimal InterestBalance { get; private set; }

        public DateTime ReferenceDate { get; }
        public decimal AmountInterestPayments { get; private set; }
        public void InterestPayments() => AmountInterestPayments += Balance * (InterestBalance / 100) / 365;

        public void UpdatePercents()
        {
            if (Balance < 50000)
            {
                InterestBalance = Bank.BankInterest.MinimalDepositPercent;
            }
            else if (Balance < 100000)
            {
                InterestBalance = Bank.BankInterest.MiddleDepositPercent;
            }
            else
            {
                InterestBalance = Bank.BankInterest.MaximumDepositPercent;
            }
        }

        public override void WithdrawMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");
            if (DateTime.Now.ToShortDateString() != ReferenceDate.ToShortDateString())
                throw new BanksException("You can't withdraw money");
            if (AccountOwner.IsDoubtfulClient)
            {
                if (money > Bank.BankInterest.TransactionLimit)
                    throw new BanksException("You can't withdraw money more than limit");
            }

            if (Balance < money)
                throw new BanksException("Can't withdraw money");

            Balance -= money;
        }

        public override void DepositMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");

            Balance += money;
        }

        public override void AddCommission(int days)
        {
            DepositMoney(AmountInterestPayments);
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