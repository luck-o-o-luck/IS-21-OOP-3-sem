using System;
using Banks.Models;
using Banks.Models.Banks;

namespace Banks.Commands
{
    public class ConsoleIO
    {
        private CentralBank _centralBank = CentralBank.GetInstance();

        public void Input(string[] args)
        {
            bool input = true;

            Console.WriteLine("Commands for done:\n\n" +
                              "/createClient - Name, Passport, Name of bank. If you want, you can set address and number\n\n" +
                              "/createBank - Name Interests(debit percents, transaction limit, three deposit percents and credit commission\n\n" +
                              "/createCentralBank\n\n" +
                              "/createAccount\n\n" +
                              "/changeBankInterests\n\n" +
                              "/changeDateCentralBankCommand\n\n" +
                              "/tryUpdatePayments\n\n" +
                              "/cancelTransaction\n\n" +
                              "/depositMoney\n\n" +
                              "/remittanceToAccount\n\n" +
                              "/withdrawMoney\n\n" +
                              "/break\n\n");

            while (input)
            {
                ICommand command;
                switch (Console.ReadLine())
                {
                    case "/createClient":
                        command = new CreateClientCommand(Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
                        command.Execute();
                        break;
                    case "/createBank":
                        command = new CreateBankCommand(
                            Console.ReadLine(),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()),
                            Convert.ToDecimal(Console.ReadLine()));
                        command.Execute();
                        break;
                    case "/createCentralBank":
                        command = new CreateCentralBankCommand();
                        command.Execute();
                        break;
                    case "/createAccount":
                        command = new CreateAccountCommand();
                        command.Execute();
                        break;
                    case "/changeBankInterests":
                        command = new ChangeBankInterestsCommand();
                        command.Execute();
                        break;
                    case "/changeDateCentralBankCommand":
                        command = new ChangeDateCentralBankCommand();
                        command.Execute();
                        break;
                    case "/tryUpdatePayments":
                        command = new TryUpdatePaymentsCommand();
                        command.Execute();
                        break;
                    case "/cancelTransaction":
                        command = new CancelTransactionCommand();
                        command.Execute();
                        break;
                    case "/depositMoney":
                        command = new DepositMoneyCommand();
                        command.Execute();
                        break;
                    case "/remittanceToAccount":
                        command = new RemittanceToAccountCommand();
                        command.Execute();
                        break;
                    case "/withdrawMoney":
                        command = new WithdrawMoneyCommand();
                        command.Execute();
                        break;
                    case "/break":
                        input = false;
                        break;
                }
            }
        }
    }
}