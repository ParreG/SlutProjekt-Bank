using SlutProjekt_Bank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlutProjekt_Bank.Classes
{
    public class Bank
    {

        // HÄR ÄR MENYN
        public void ShowMenu()
        {
            AccountServices accountServices = new AccountServices();
            bool programAktivt = true;
            int menuSelected = 0;
            string[] menuOptions = new string[] { "Inloggning", "Registrering", "Avsluta programmet" };
            int menuWidth = 35; // Adjust this value to change the menu width

            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔" + new string('═', menuWidth + 16) + "╗");
                Console.WriteLine("║" + "\t\tVälkommen till Menyn".PadLeft((menuWidth + "Välkommen till Menyn".Length) / 2).PadRight(menuWidth + 8) + "║");
                Console.WriteLine("╚" + new string('═', menuWidth + 16) + "╝");
                Console.ResetColor();

                Console.WriteLine("Hej och välkommen till bankens meny.");
                Console.WriteLine("Du kan navigera med \" ⬇️\" och \" ⬆️\".");
                Console.WriteLine("Tryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();
                Console.WriteLine("╔" + new string('═', menuWidth - 2) + "╗");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {

                        // Set the default color for the border
                        Console.ResetColor();
                        Console.Write("║ ");

                        // Set the color for the text
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> " + menuOptions[i].PadRight(menuWidth - 8) + " <");

                        // Change back to the default color for the closing border
                        Console.ResetColor();
                        Console.WriteLine(" ║");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("║   " + menuOptions[i].PadRight(menuWidth - 5) + "║");
                    }
                }
                Console.WriteLine("╚" + new string('═', menuWidth - 2) + "╝");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuSelected < menuOptions.Length - 1)
                            menuSelected++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (menuSelected > 0)
                            menuSelected--;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (menuSelected)
                        {
                            case 0: // inloggning
                                Console.WriteLine("Du valde Inloggning");
                                var user = Security.Login();
                                if (user != null)
                                {
                                    Console.WriteLine($"Välkommen, {user.Name} {user.Surname}!");
                                    ShowLoggedInMenu(user);
                                }
                                else
                                {
                                    Console.WriteLine("Inloggning misslyckades.");
                                }
                                break;
                            case 1: // Registrering
                                Console.WriteLine("Du valde Registrering");
                                Security.Register();
                                break;

                            case 2: // Avsluta
                                Console.WriteLine("Avslutar programmet...");
                                programAktivt = false;
                                break;
                        }
                        if (programAktivt)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn!");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }


        }
        public void ShowLoggedInMenu(User user)
        {

            bool programAktivt = true;
            int menuSelected = 0;
            string[] menuOptions = new string[] { "Visa kontoinformation", "Öppna konto", "låna pengar", "Överför pengar", "Logga ut" };
            int menuWidth = 35;
            AccountServices accountServices = new AccountServices();

            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔" + new string('═', menuWidth + 16) + "╗");
                Console.WriteLine("║" + $"\t\tVälkommen {user.Name} {user.Surname}".PadLeft((menuWidth + $"Välkommen {user.Name} {user.Surname}".Length) / 2).PadRight(menuWidth + 8) + "║");
                Console.WriteLine("╚" + new string('═', menuWidth + 16) + "╝");
                Console.ResetColor();

                Console.WriteLine("Hej och välkommen till bankens inloggade meny.");
                Console.WriteLine("Du kan navigera med \"⬇️\" och \"⬆️\".");
                Console.WriteLine("Tryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();
                Console.WriteLine("╔" + new string('═', menuWidth - 2) + "╗");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        Console.ResetColor();
                        Console.Write("║ ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> " + menuOptions[i].PadRight(menuWidth - 8) + " <");
                        Console.ResetColor();
                        Console.WriteLine(" ║");
                    }
                    else
                    {
                        Console.WriteLine("║   " + menuOptions[i].PadRight(menuWidth - 5) + "║");
                    }
                }
                Console.WriteLine("╚" + new string('═', menuWidth - 2) + "╝");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuSelected < menuOptions.Length - 1)
                            menuSelected++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (menuSelected > 0)
                            menuSelected--;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (menuSelected)
                        {
                            case 0: //Account history
                                accountServices.displayAccounts();
                                break;

                            case 1: // Skapa konto, spar/vanlig
                                accountServices.AccountManager();
                                break;

                            case 2: // Ta ett lån
                                accountServices.Loan(accountServices.Accounts);
                                break;

                            case 3: // meny för överföring
                                ShowLoggedInTransferMenu(user, accountServices);
                                break;

                            case 4: // Utloggning, Tillbaka till menyn innan!
                                Console.WriteLine("Loggar ut! Vänta några sekunder!");
                                Thread.Sleep(3000);
                                programAktivt = false;
                                break;
                        }
                        if (programAktivt)
                        {
                            Console.WriteLine(); //För extra utrymme
                            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn!");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
        }
        public void ShowLoggedInTransferMenu(User user, AccountServices accountServices)
        {

            bool programAktivt = true;
            int menuSelected = 0;
            string[] menuOptions = new string[] { "Överföra till annan användare", "Överföra till eget konto", "Sätt in pengar", "Ta ut pengar", "Tillbaka" };
            int menuWidth = 35;


            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔" + new string('═', menuWidth + 16) + "╗");
                Console.WriteLine("║" + $"\t\tVälkommen {user.Name} {user.Surname}".PadLeft((menuWidth + $"Välkommen {user.Name} {user.Surname}".Length) / 2).PadRight(menuWidth + 8) + "║");
                Console.WriteLine("╚" + new string('═', menuWidth + 16) + "╝");
                Console.ResetColor();

                Console.WriteLine("Hej och välkommen till bankens inloggade meny.");
                Console.WriteLine("Du kan navigera med \"⬇️\" och \"⬆️\".");
                Console.WriteLine("Tryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();
                Console.WriteLine("╔" + new string('═', menuWidth - 2) + "╗");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        Console.ResetColor();
                        Console.Write("║ ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("> " + menuOptions[i].PadRight(menuWidth - 8) + " <");
                        Console.ResetColor();
                        Console.WriteLine(" ║");
                    }
                    else
                    {
                        Console.WriteLine("║   " + menuOptions[i].PadRight(menuWidth - 5) + "║");
                    }
                }
                Console.WriteLine("╚" + new string('═', menuWidth - 2) + "╝");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuSelected < menuOptions.Length - 1)
                            menuSelected++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (menuSelected > 0)
                            menuSelected--;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (menuSelected)
                        {
                            case 0:
                                Console.WriteLine("Överföra till annan användare");
                                break;

                            case 1:
                                Console.WriteLine("Överföra till eget konto");
                                accountServices.PersonalTransfer();
                                break;

                            case 2:
                                Console.WriteLine("Sätt in pengar");
                                accountServices.Deposit();
                                break;

                            case 3:
                                Console.WriteLine("Ta ut pengar");
                                accountServices.Withdraw();
                                break;

                            case 4:
                                programAktivt = false;
                                break;

                        }
                        if (programAktivt)
                        {
                            Console.WriteLine(); //För extra utrymme
                            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn!");
                            Console.ReadKey(true);
                        }
                        break;
                }

            }
        }
    }
}