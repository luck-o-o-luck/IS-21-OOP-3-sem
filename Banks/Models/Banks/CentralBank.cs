using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Models.Accounts;
using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Banks
{
    public class CentralBank
    {
        private static CentralBank _instance = null;
        private List<Bank> _banks;
        private List<Client> _clients;
        private List<Transaction<Account>> _transactions;

        private CentralBank()
        {
            _banks = new List<Bank>();
            _clients = new List<Client>();
            _transactions = new List<Transaction<Account>>();
            DateTimeProvider = new DateTimeProvider();
        }

        public DateTimeProvider DateTimeProvider { get; }
        public bool CentralBankExists => !(_instance is null);
        public static CentralBank GetInstance()
        {
            _instance ??= new CentralBank();
            return _instance;
        }

        public Bank FindBank(string name) => _banks.SingleOrDefault(bank => bank.Name == name);

        public Transaction<Account> FindTransaction(string id) =>
            _transactions.SingleOrDefault(transaction => transaction.Id.ToString() == id);
        public Client FindClient(string passport, Bank bank) =>
            _banks.Single(x => x.Name == bank.Name).Clients.Single(x => x.Passport == passport);
        public bool ClientExists(string passport, Bank bank) =>
            _banks.Single(x => x.Name == bank.Name).Clients.Any(x => x.Passport == passport);
        public bool BankExists(string name) => _banks.Any(bank => bank.Name == name);
        public Client CreateClient(string fullName, string passport, Bank bank)
        {
            var client = new ClientBuilder().SetFullName(fullName).SetPassport(passport).Create();
            _clients.Add(client);
            bank.AddClientForBank(client);

            return client;
        }

        public Bank CreateBank(string name, BankInterest bankInterest)
        {
            if (bankInterest is null)
                throw new BanksException("Bank is null");
            if (string.IsNullOrEmpty(name))
                throw new BanksException("Name is null");
            if (BankExists(name))
                throw new BanksException("This bank already exists");

            var bank = new Bank(name, bankInterest);

            _banks.Add(bank);
            return bank;
        }

        public void ChangeDebitPercent(decimal newPercent, Bank bank)
        {
            bank.BankInterest.ChangeDebitPercent(newPercent);
            bank.Notify("Debit percent", newPercent);
        }

        public void ChangeTransactionLimit(decimal newLimit, Bank bank)
        {
            bank.BankInterest.ChangeTransactionLimit(newLimit);
            bank.Notify("Transaction limit", newLimit);
        }

        public void ChangeMinimalDepositPercent(decimal newPercent, Bank bank)
        {
            bank.BankInterest.ChangeMinimalDepositPercent(newPercent);
            bank.Notify("Minimal deposit percent", newPercent);
        }

        public void ChangeMiddleDepositPercent(decimal newPercent, Bank bank)
        {
            bank.BankInterest.ChangeMiddleDepositPercent(newPercent);
            bank.Notify("Middle deposit percent", newPercent);
        }

        public void ChangeMaximumDepositPercent(decimal newPercent, Bank bank)
        {
            bank.BankInterest.ChangeMaximumDepositPercent(newPercent);
            bank.Notify("Maximum deposit percent", newPercent);
        }

        public void ChangeCreditCommission(decimal newCommission, Bank bank)
        {
            bank.BankInterest.ChangeCreditCommission(newCommission);
            bank.Notify("Credit commission", newCommission);
        }

        public void RewindTimeDays(int days)
        {
            DateTimeProvider.RewindTimeDays(days);
        }

        public void TryUpdatePayments()
        {
            var accounts = _banks.SelectMany(bank => @bank.Accounts);

            foreach (Account account in accounts)
                account.UpdateCommissions(DateTimeProvider.TimeSinceLastUpdate.Days);

            if (!DateTimeProvider.TimeToUpdatePayments)
                throw new BanksException("It's too early to update");

            foreach (Account account in accounts)
                account.AddCommission(DateTimeProvider.TimeSinceLastUpdate.Days);

            DateTimeProvider.UpdateLastUpdateTime();
        }

        public Transaction<Account> CreateTransaction(Account account, Bank bank, decimal money)
        {
            var transaction = new Transaction<Account>(account, money);
            _transactions.Add(transaction);

            return transaction;
        }

        public void CancelTransaction(string id)
        {
            var transaction = FindTransaction(id);

            if (transaction is null)
                throw new BanksException("Transaction doesn't exist");

            transaction.Cancel();
        }
    }
}