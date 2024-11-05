using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public static class Security
    {
        // Dictionary to store email as the key and password as the value
        public static List<User> Users = new();

        public static bool AuthenticateUser()
        {
            int failedAttempts = 0;
            DateTime? lockoutEndTime = null;

            while (true)
            {
                // Check if the user is currently locked out
                if (failedAttempts >= 3 && lockoutEndTime.HasValue && DateTime.Now < lockoutEndTime.Value)
                {
                    Console.WriteLine($"Too many failed attempts. Please wait {Math.Ceiling((lockoutEndTime.Value - DateTime.Now).TotalSeconds)} seconds before trying again.");
                    Thread.Sleep((int)(lockoutEndTime.Value - DateTime.Now).TotalMilliseconds);
                    continue;
                }

                // Reset failed attempts after lockout period ends
                if (lockoutEndTime.HasValue && DateTime.Now >= lockoutEndTime.Value)
                {
                    failedAttempts = 0;
                    lockoutEndTime = null;
                }

                Console.Write("Email: ");
                string inputEmail = Console.ReadLine();
                Console.Write("Password: ");
                string inputPassword = Console.ReadLine();

                // Find a user with the matching email
                var user = Users.FirstOrDefault(u => u.Email == inputEmail);

                // Check if the user exists and passwords match
                if (user != null && user.Password == inputPassword)
                {
                    Console.WriteLine("Login successful");
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid email or password.");
                    failedAttempts++;

                    // If 3 failed attempts, start a 1-minute lockout
                    if (failedAttempts >= 3)
                    {
                        lockoutEndTime = DateTime.Now.AddMinutes(1);
                        Console.WriteLine("Too many failed attempts. You are locked out for 1 minute.");
                    }
                }
            }
        }


        public static bool Login()
        {
            Console.WriteLine("Login");
            if (AuthenticateUser())
            {
                Console.WriteLine("Login successful");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed");
                return false;
            }
        }
    }
}
