using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Admin(string password, string name, string surName, string email, string accountType)
        : User(password, name, surName, email, accountType)
    {

        // Create a new client
        public void CreateClient()
        {
            Console.WriteLine("Name: "); string name = Console.ReadLine();
            Console.WriteLine("Surname: "); string surName = Console.ReadLine();
            Console.WriteLine("Password: "); string password = Console.ReadLine();
            Console.WriteLine("Email: "); string email = Console.ReadLine();
            Client client = new Client(password, name, surName, email, "User");
            Security.Users.Add(client);
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
