using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public abstract class User(string password, string name, string surName, string email, int phoneNumber, string address, int postalCode, string city, string country)
    {
        public Guid UserID = Guid.NewGuid();
        public string Password = password;
        public string Name = name;
        public string Surname = surName;
        public string Email = email;
        public int Phone = phoneNumber;
        public string Address = address;
        public int PostalCode = postalCode;
        public string City = city;
        public string Country = country;

        // Prints the user information
        public void UserInformation()
        {
            Console.WriteLine("User ID: " + UserID);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Surname: " + Surname);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Phone: " + Phone);
        }
    }
}