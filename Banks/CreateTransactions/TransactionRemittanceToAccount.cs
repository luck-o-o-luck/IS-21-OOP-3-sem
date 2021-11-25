using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models
{
    public class TransactionRemittanceToAccount : Transaction
    {
        public TransactionRemittanceToAccount(Account accountFromTransaction, Account accountForRemittance, decimal money)
            : base(accountFromTransaction, money)
        {
            AccountForRemittance = accountForRemittance ?? throw new BanksException("Account is null");
        }

        public Account AccountForRemittance { get; }

        public override void TransactionWithAccount()
        {
            Account.WithdrawMoney(Money);
            AccountForRemittance.DepositMoney(Money);
            CompletedTransaction = true;
        }

        public override void Cancel()
        {
            if (!CompletedTransaction)
                throw new BanksException("Transaction isn't complete");
            if (CanceledTransaction)
                throw new BanksException("Transaction already canceled");

            Account.DepositMoney(Money);
            AccountForRemittance.WithdrawMoney(Money);
            CanceledTransaction = true;
        }
    }
}