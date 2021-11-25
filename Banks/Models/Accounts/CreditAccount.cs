using System;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(Client client, Bank bank)
            : base(bank.BankInterest.CreditLimit, client, bank)
        { }

        public decimal Commission => Bank.BankInterest.CreditCommission;
        public decimal AccumulatedCommission { get; private set; } = 0;
        public bool IsEnoughMoneyToWithdraw(decimal money) => Balance - money >= Bank.BankInterest.CreditCommission ||
                                                              (money > Balance && Balance - money - Commission >= Bank.BankInterest.CreditCommission);
        public override void WithdrawMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");

            if (!IsEnoughMoneyToWithdraw(money))
                throw new BanksException("Isn't enough money to withdraw");
            if (AccountOwner.IsDoubtfulClient)
            {
                if (money > Bank.BankInterest.TransactionLimit)
                    throw new BanksException("You can't withdraw money more than limit");
            }

            if (money <= Balance)
                Balance -= money;

            if (money > Balance)
                Balance = Balance - money - Commission;
        }

        public override void DepositMoney(decimal money)
        {
            if (money < 0)
                throw new BanksException("Money less than zero");

            Balance += money;
        }

        public override void AddCommission(int days)
        {
            if (days < 0)
                throw new BanksException("Days can't be less than zero");

            Balance -= AccumulatedCommission;
        }

        public override void UpdateCommissions(int days)
        {
            if (days < 0)
                throw new BanksException("Days can't be less than zero");

            for (int i = 0; i < days; i++)
                AccumulatedCommission += Commission;
        }
    }
}