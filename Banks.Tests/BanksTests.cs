using System;
using Banks.Models;
using Banks.Models.Accounts;
using Banks.Models.Banks;
using Banks.Models.Clients;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTests
    {
        [Test]
        public void CreateClient_ClientHasInformation()
        {
            var builder = new ClientBuilder();
            var client = builder.SetFullName("Васютинская Ксения").SetPassport("1234567890").Create();
            
            Assert.AreEqual(client.FullName, "Васютинская Ксения");
            Assert.AreEqual(client.Passport, "1234567890");
            Assert.AreEqual(client.IsDoubtfulClient, true);
            
            client.SetAddress(new Address("улица Пушкина", 1, 23));
            client.SetNumberPhone("89213674849");
            
            Assert.AreEqual(client.Address.Street, "улица Пушкина");
            Assert.AreEqual(client.NumberPhone, "89213674849"); 
        }

        [Test]
        public void CreateBank_BankHasInformation()
        {
            var interests = new BankInterest(1, 1000, 14567,2, 3, 4,  560);
            var bank = new Bank("Tinkoff", interests);
            
            Assert.AreEqual(bank.Name, "Tinkoff");
            Assert.AreEqual(bank.BankInterest, interests);
        }

        [Test]
        public void CreateAccountDoubtfulClient_ClientCantWithdrawMoneyWithoutLimit()
        {
            var builder = new ClientBuilder();
            var client = builder.SetFullName("Васютинская Ксения").SetPassport("1234567890").Create();
            
            var interests = new BankInterest(1, 1000, 14577,2, 3, 4, 560);
            var bank = new Bank("Tinkoff", interests);

            var account = new DebitAccount(1560, client, bank);
            
            Assert.Catch<BanksException>(() =>
            {
                account.WithdrawMoney(15000);
            });
        }

        [Test]
        public void ChangeInterests_BankHasNewInterests()
        {
            var centralBank = CentralBank.GetInstance();
            var interests = new BankInterest(1, 1000, 145678,2, 3, 4,  560);
            var bank = new Bank("Tinkoff", interests);

            centralBank.ChangeCreditCommission(123456, bank);
            Assert.AreEqual(bank.BankInterest.CreditCommission, 123456);
            
            centralBank.ChangeDebitPercent(2, bank);
            Assert.AreEqual(bank.BankInterest.DebitPercent, 2);
            
            centralBank.ChangeMinimalDepositPercent(1, bank);
            Assert.AreEqual(bank.BankInterest.MinimalDepositPercent, 1);
            
            centralBank.ChangeMiddleDepositPercent(2, bank);
            Assert.AreEqual(bank.BankInterest.MiddleDepositPercent, 2);
            
            centralBank.ChangeMaximumDepositPercent(3, bank);
            Assert.AreEqual(bank.BankInterest.MaximumDepositPercent, 3);
            
            centralBank.ChangeTransactionLimit(12222222222, bank);
            Assert.AreEqual(bank.BankInterest.TransactionLimit, 12222222222);
        }

        [Test]
        public void TransactionWithOthersAccounts()
        {
            var builder = new ClientBuilder();
            var client = builder.SetFullName("Васютинская Ксения").SetPassport("1234567890").Create();
            
            var interests = new BankInterest(1, 1000, 145678,2, 3, 4, 560);
            var bank = new Bank("Tinkoff", interests);

            var debitAccount = new DebitAccount(1560, client, bank);
            var debitAccount2 = new DebitAccount(1560, client, bank);
            var creditAccount = new CreditAccount(client, bank);
            var depositAccount = new DepositAccount(12345, client, new DateTime(2021, 12, 2), bank);

            var transaction = new Transaction<DebitAccount>(debitAccount, 100);
            transaction.RemittanceToAccount(debitAccount2);
            transaction.RemittanceToAccount(creditAccount);
            transaction.RemittanceToAccount(depositAccount);
            
            Assert.AreEqual(debitAccount.Balance, 1260);
            Assert.AreEqual(debitAccount2.Balance, 1660);
            Assert.AreEqual(creditAccount.Balance, 1100);
            Assert.AreEqual(depositAccount.Balance, 12445);
            
            Assert.Catch<BanksException>(() =>
            {
                depositAccount.WithdrawMoney(1500);
            });
        }

        [Test]
        public void ChangeTimeForCentralBank_UpdatePayments()
        {
            var centralBank = CentralBank.GetInstance();
            var interests = new BankInterest(1, 1000, 145678,2, 3, 4, 560);
            var bank = centralBank.CreateBank("Tinkoff", interests);
            var client = centralBank.CreateClient("Васютинская", "1234567990", bank);

            var debitAccount = new DebitAccount(1560, client, bank);
            bank.AddAccountForBank(debitAccount);
            centralBank.RewindTimeDays(46);
            centralBank.TryUpdatePayments();

            Assert.AreEqual(debitAccount.Balance, 2277.6);
        }
    }
}