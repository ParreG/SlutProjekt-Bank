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
                Console.WriteLine("╔═════════════════════════════════╗");
                Console.WriteLine("║" + "Välkommen till Menyn".PadLeft((menuWidth + "Välkommen till Menyn".Length) / 2).PadRight(menuWidth - 2) + "║");
                Console.WriteLine("╚" + new string('═', menuWidth - 2) + "╝");
                Console.ResetColor();

                Console.WriteLine("Hej och välkommen till bankens meny.");
                Console.WriteLine("Du kan navigera med \" ⬇️\" och \" ⬆️\".");
                Console.WriteLine("Tryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();
                Console.WriteLine("╔═════════════════════════════════╗");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("║ > " + menuOptions[i].PadRight(menuWidth - 7) + " <║");
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
                            case 0:
                                Console.WriteLine("Du valde Inloggning");
                                // Implement login logic here
                                break;
                            case 1:
                                Console.WriteLine("Du valde Registrering");
                                // Implement registration logic here
                                break;
                            case 2:
                                Console.WriteLine("Avslutar programmet...");
                                programAktivt = false;
                                break;
                        }
                        if (programAktivt)
                        {
                            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
        }



    }
}
