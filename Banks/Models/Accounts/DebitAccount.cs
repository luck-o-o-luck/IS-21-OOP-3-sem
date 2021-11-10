using Banks.Models.Clients;

namespace Banks.Models.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(int id, decimal balance, Client client)
            : base(id, balance, client) { }
    }
}