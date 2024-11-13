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
            new Client("123", "Team", "Banken", "123")
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
                    Console.WriteLine($"För många misslyckade försök. Vänligen vänta {Math.Ceiling((lockoutEndTime.Value - DateTime.Now).TotalSeconds)} sekunder innan du försöker igen.");
                    Thread.Sleep((int)(lockoutEndTime.Value - DateTime.Now).TotalMilliseconds);
                    continue;
                }

                // Reset failed attempts after lockout period ends
                if (lockoutEndTime.HasValue && DateTime.Now >= lockoutEndTime.Value)
                {
                    failedAttempts = 0;
                    lockoutEndTime = null;
                }

                Console.Write("E-post: ");
                string inputEmail = Console.ReadLine();
                Console.Write("Lösenord: ");
                string inputPassword = Console.ReadLine();

                // Find a user with the matching email
                var user = Users.FirstOrDefault(u => u.Email == inputEmail);

                // Check if the user exists and passwords match
                if (user != null && user.Password == inputPassword)
                {
                    Console.WriteLine("Inloggning lyckades!");
                    return user;
                }
                else
                {
                    Console.WriteLine("Ogiltig e-post eller lösenord.");
                    failedAttempts++;

                    // If 3 failed attempts, start a 1-minute lockout
                    if (failedAttempts >= 3)
                    {
                        lockoutEndTime = DateTime.Now.AddMinutes(1);
                        Console.WriteLine("För många misslyckade försök. Du är spärrad i 1 minut.");
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
            Console.WriteLine();
            newUser.UserInformation();
        }
    }
}