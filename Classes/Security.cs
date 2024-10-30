using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Security
    {
        // Dictionary to store email as the key and password as the value
        private Dictionary<string, string> users = new();

        // Constructor to add some test users (Optional)
        public Security()
        {
            users.Add("user1@example.com", "password123");
            users.Add("user2@example.com", "securePassword");
            users.Add("user3@example.com", "myPassword");
        }

        // Function to authenticate the user
        public bool AuthenticateUser(string email, string password)
        {
            // Check if the email exists in the dictionary
            if (users.TryGetValue(email, out string storedPassword))
            {
                // Compare the stored password with the provided password
                return storedPassword == password;
            }
            else
            {
                // If email not found, return false
                Console.WriteLine("User not found.");
                return false;
            }
        }
    }
}
