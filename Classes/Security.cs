using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public static class Security
    {
        // Dictionary to store email as the key and password as the value
        private static Dictionary<string, string> users = new();

        // Function to authenticate the user
        public static bool AuthenticateUser(string email, string password)
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
