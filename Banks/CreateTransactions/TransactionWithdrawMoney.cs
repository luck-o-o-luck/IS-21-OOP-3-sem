using Banks.Models.Accounts;
using Banks.Tools;

namespace Banks.Models
{
    public class TransactionWithdrawMoney : Transaction
    {
        public TransactionWithdrawMoney(Account account, decimal money)
            : base(account, money)
        {
        }

        public override void TransactionWithAccount()
        {
            Account.WithdrawMoney(Money);
            CompletedTransaction = true;
        }

        public override void Cancel()
        {
            if (!CompletedTransaction)
                throw new BanksException("Transaction isn't complete");
            if (CanceledTransaction)
                throw new BanksException("Transaction already canceled");

            Account.DepositMoney(Money);
            CanceledTransaction = true;
        }
    }
}