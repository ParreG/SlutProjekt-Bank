using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Client(string password, string name, string surName, string email) 
        : User(password, name, surName, email)
    {
        public List<Account> Accounts { get; set; }

        public void CreateAccount(Client client,decimal initialBalance, string currency, string accountName)
        {
            Account account = new Account(client, initialBalance, currency, accountName);
        }

        public void Deposit(Account account, decimal amount)
        {
            account.Balance += amount;
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot");
            }
        }

        public void Transfer(Account account1, Account account2, decimal amount)
        {
            if (account1.Balance >= amount)
            {
                account1.Balance -= amount;
                account2.Balance += amount;
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot");
            }

        }

        public void ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
        }

        public void ChangeEmail(User user, string newEmail)
        {
            user.Email = newEmail;
        }

        public void ChangePhoneNumber(User user, int newPhoneNumber)
        {
            user.Phone = newPhoneNumber;
        }

        //public void ChangeAddress(User user, string newAddress)
        //{
        //    user.Address = newAddress;
        //}

        //public void ChangePostalCode(User user, int newPostalCode)
        //{
        //    user.PostalCode = newPostalCode;
        //}

        //public void ChangeCity(User user, string newCity)
        //{
        //    user.City = newCity;
        //}

        //public void ChangeCountry(User user, string newCountry)
        //{
        //    user.Country = newCountry;
        //}
    }
}