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
        // List of users
        public static List<User> Users = new()
        {
            new Client("123", "Jack", "Dorsay", "Jack7000")
        };

        public static User Login()
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
                    return user;
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
        public static void Register()
        {
            Console.Write("Ange förnamn: ");
            string name = Console.ReadLine();

            Console.Write("Ange efternamn: ");
            string surname = Console.ReadLine();

            Console.Write("Ange email: ");
            string email = Console.ReadLine();

            Console.Write("Ange lösenord: ");
            string password = Console.ReadLine();

            // Kontrollera om användaren redan existerar baserat på e-post
            if (Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("En användare med denna e-postadress finns redan.");
                return;
            }

            // Skapa en ny användare
            var newUser = new Client(password, name, surname, email);
            Users.Add(newUser);

            Console.WriteLine("Registrering lyckades! Här är din information:");
            newUser.UserInformation();
        }
    }
}