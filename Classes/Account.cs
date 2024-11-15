using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Account 
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; }
        public User AccountHolder { get; set; }

        public Account(User accountHolder, decimal balance, string currency, string accountName)
        {
            Balance = balance;
            Currency = currency;
            AccountName = accountName;
            AccountHolder = accountHolder;
        }

    }

    


}
