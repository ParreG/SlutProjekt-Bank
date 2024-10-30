using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Admin(string userName, string password, string name, string surName, string email, int phoneNumber, string address, int postalCode, string city, string country)
        : User(password, name, surName, email, phoneNumber, address, postalCode, city, country)
    {

        // Create a new client
        public void CreateClient()
        {
            Console.WriteLine("Username: "); string userName = Console.ReadLine();
            Console.WriteLine("Password: "); string password = Console.ReadLine();
            Console.WriteLine("Name: "); string name = Console.ReadLine();
            Console.WriteLine("Surname: "); string surName = Console.ReadLine();
            Console.WriteLine("Email: "); string email = Console.ReadLine();
            Console.WriteLine("Phone number: "); int phoneNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Address: "); string address = Console.ReadLine();
            Console.WriteLine("Postal code: "); int postalCode = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("City: "); string city = Console.ReadLine();
            Console.WriteLine("Country: "); string country = Console.ReadLine();
            Client client = new Client(userName, password, name, surName, email, phoneNumber, address, postalCode, city, country);
        }

        public void DeleteClient(Client client)
        {
            client = null;
        }

        public void DeleteAccount(Account account)
        {
            account = null;
        }
    }
}
