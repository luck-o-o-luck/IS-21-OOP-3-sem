using System;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class CreateCentralBankCommand : ICommand
    {
        public CentralBank CentralBank { get; private set; }
        public void Execute()
        {
            if (CentralBank.CentralBankExists)
            {
                Console.WriteLine("Central bank already exists");
                return;
            }

            CentralBank = CentralBank.GetInstance();
        }
    }
}