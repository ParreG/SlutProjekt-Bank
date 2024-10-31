using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Account : Client
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; }
        public Account(Client client, decimal balance, string currency, string accountName)
            : base(client.UserName, client.Password, client.Name, client.Surname, client.Email, client.Phone, client.Address, client.PostalCode, client.City, client.Country)
        {
            Balance = balance;
            Currency = currency;
            AccountName = accountName;
        }

        public void SavingAccount(Client client, decimal balance, string currency)
        {
            Console.WriteLine("Vad härligt att du vill öppna ett sparkonto.");
            Console.WriteLine("Vad ska sparkontot heta: ");
            string savingAccountName = Console.ReadLine();
            Account savingAccount = new Account(client, balance, currency, savingAccountName);


            // Hur mycket ska överföras till kontot? 
            // från vilket konto?

            //Måste ta med TransferMoney(); ?
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Current balance: {Balance}");
        }

    }
}
