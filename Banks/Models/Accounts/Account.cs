using Banks.Models.Clients;
using Banks.Tools;

namespace Banks.Models.Accounts
{
    public abstract class Account
    {
        protected Account(int id, decimal balance, Client client)
        {
            if (id < 0)
                throw new BanksException("Invalid id");
            if (balance < 0)
                throw new BanksException("Invalid balance");

            Id = id;
            Balance = balance;
            AccountOwner = client ?? throw new BanksException("Can't created account without client");
        }

        public int Id { get; }
        public decimal Balance { get; } = 0;
        public Client AccountOwner { get; }
    }
}