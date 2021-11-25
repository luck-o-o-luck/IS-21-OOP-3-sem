using System;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class CreateBankCommand : ICommand
    {
        private string _name;
        private BankInterest _bankInterest;

        public CreateBankCommand(
            string name,
            decimal debitPercent,
            decimal creditLimit,
            decimal transactionLimit,
            decimal minimalDepositPercent,
            decimal middleDepositPercent,
            decimal maximumDepositPercent,
            decimal creditCommission)
        {
            _name = name;
            _bankInterest = new BankInterest(
                debitPercent,
                creditLimit,
                transactionLimit,
                minimalDepositPercent,
                middleDepositPercent,
                maximumDepositPercent,
                creditCommission);
        }

        public void Execute()
        {
            var bank = CentralBank.GetInstance().CreateBank(_name, _bankInterest);

            Console.WriteLine($"Bank {bank.ToString()} creates");
        }
    }
}