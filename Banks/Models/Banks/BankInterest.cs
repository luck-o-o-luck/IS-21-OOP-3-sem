using Banks.Tools;

namespace Banks.Models.Banks
{
    public class BankInterest
    {
        private const int PercentageLimit = 6;
        public BankInterest(
            decimal debitPercent,
            decimal creditLimit,
            decimal transactionLimit,
            decimal minimalDepositPercent,
            decimal middleDepositPercent,
            decimal maximumDepositPercent,
            decimal creditCommission)
        {
            if (debitPercent > PercentageLimit)
                throw new BanksException("This debit interest on balance can't be created");
            if (minimalDepositPercent > PercentageLimit)
                throw new BanksException("This deposit interest on balance can't be created");
            if (middleDepositPercent > PercentageLimit)
                throw new BanksException("This deposit interest on balance can't be created");
            if (maximumDepositPercent > PercentageLimit)
                throw new BanksException("This deposit interest on balance can't be created");

            if (minimalDepositPercent > middleDepositPercent || minimalDepositPercent > maximumDepositPercent)
                throw new BanksException("Minimal deposit percent can't be more than other");
            if (middleDepositPercent > maximumDepositPercent)
                throw new BanksException("Middle deposit percent can't be more than maximum deposit percent");

            if (debitPercent < 0)
                throw new BanksException("This debit interest on balance can't be created");
            if (minimalDepositPercent < 0)
                throw new BanksException("This deposit interest on balance can't be created");
            if (middleDepositPercent < 0)
                throw new BanksException("This deposit interest on balance can't be created");
            if (maximumDepositPercent < 0)
                throw new BanksException("This deposit interest on balance can't be created");
            if (transactionLimit < 0)
                throw new BanksException("This transaction on limit on balance can't be created");
            if (creditCommission < 0)
                throw new BanksException("This credit commission can't be created");
            if (creditLimit < 0)
                throw new BanksException("This credit commission can't be created");

            DebitPercent = debitPercent;
            CreditLimit = creditLimit;
            TransactionLimit = transactionLimit;
            MinimalDepositPercent = minimalDepositPercent;
            MiddleDepositPercent = middleDepositPercent;
            MaximumDepositPercent = maximumDepositPercent;
            CreditCommission = creditCommission;
        }

        public decimal DebitPercent { get; private set; }
        public decimal CreditLimit { get; private set; }
        public decimal TransactionLimit { get; private set; }
        public decimal MinimalDepositPercent { get; private set; }
        public decimal MiddleDepositPercent { get; private set; }
        public decimal MaximumDepositPercent { get; private set; }
        public decimal CreditCommission { get; private set; }

        public void ChangeDebitPercent(decimal newPercent)
        {
            if (newPercent < 0)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > PercentageLimit)
                throw new BanksException("This debit percent can't be created");

            DebitPercent = newPercent;
        }

        public void ChangeTransactionLimit(decimal newLimit)
        {
            if (newLimit < 0)
                throw new BanksException("This debit percent can't be created");

            TransactionLimit = newLimit;
        }

        public void ChangeMinimalDepositPercent(decimal newPercent)
        {
            if (newPercent < 0)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > PercentageLimit)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > MiddleDepositPercent || newPercent > MaximumDepositPercent)
                throw new BanksException("Minimal deposit percent can't be more than other");

            MinimalDepositPercent = newPercent;
        }

        public void ChangeMiddleDepositPercent(decimal newPercent)
        {
            if (newPercent < 0)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > PercentageLimit)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > MaximumDepositPercent || newPercent < MinimalDepositPercent)
                throw new BanksException("Middle deposit percent can't be more than maximum deposit percent");

            MiddleDepositPercent = newPercent;
        }

        public void ChangeMaximumDepositPercent(decimal newPercent)
        {
            if (newPercent < 0)
                throw new BanksException("This debit percent can't be created");
            if (newPercent > PercentageLimit)
                throw new BanksException("This debit percent can't be created");
            if (newPercent < MiddleDepositPercent || newPercent < MinimalDepositPercent)
                throw new BanksException("Middle deposit percent can't be more than maximum deposit percent");

            MaximumDepositPercent = newPercent;
        }

        public void ChangeCreditCommission(decimal newCommission)
        {
            if (newCommission < 0)
                throw new BanksException("This credit commission can't be created");

            CreditCommission = newCommission;
        }
    }
}