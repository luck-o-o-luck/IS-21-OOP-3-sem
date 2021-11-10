using Banks.Models.Clients;

namespace Banks.Models.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(int id, decimal balance, Client client)
            : base(id, balance, client) { }
    }
}