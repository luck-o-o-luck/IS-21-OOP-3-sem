using System;
using Banks.Models.Clients;

namespace Banks.Models.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(int id, decimal balance, Client client)
            : base(id, balance, client) { }

        public DateTime PaymentTerm { get; private set; }
        public DateTime ReferenceDate { get; private set; }
    }
}