using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models
{
    public class TransactionDepositMoney : Transaction
    {
        public TransactionDepositMoney(Account account, decimal money)
            : base(account, money)
        {
        }

        public override void TransactionWithAccount()
        {
            Account.DepositMoney(Money);
            CompletedTransaction = true;
        }

        public override void Cancel()
        {
            if (!CompletedTransaction)
                throw new BanksException("Transaction isn't complete");
            if (CanceledTransaction)
                throw new BanksException("Transaction already canceled");

            Account.WithdrawMoney(Money);
            CanceledTransaction = true;
        }
    }
}