using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Account : User
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public Account( string name, string password, string surName, string email, int phoneNumber, string address, int postalCode, string city, string country, decimal balance, string currency) 
            : base(name, password, surName, email, phoneNumber, address, postalCode, city, country)
        {
            Balance = balance;
            Currency = currency;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        public void DisplayBalance()
        {
            Console.WriteLine($"Current balance: {Balance}");
        }
        public void TransferFunds(Account recipient, decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                recipient.Deposit(amount);
                Console.WriteLine($"Transfer of {amount} to {recipient.Name} successful.");
            }
            else
            {
                Console.WriteLine("Insufficient funds for transfer.");
            }
        }
    }
}
