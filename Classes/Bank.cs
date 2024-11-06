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
            string[] menuOptions = new string[] { "Inloggning\t\t", "Registrering\t\t", "Avsluta programmet\t" };

            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═════════════════════════════════╗");
                Console.WriteLine("║         Välkommen till Menyn    ║");
                Console.WriteLine("╚═════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("Hej och välkommen till bankens meny.");
                Console.WriteLine("Du kan navigera med \" ⬇️\" och \" ⬆️\". \nTryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();

                //Loopar igenom menyvalen och markerar det valda alternativet. Detta gör att jag inte behöver repetera samma del flera gånger. 
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        //För att menyn ska synas bättre la jag till en pil innan menyn också
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"║ > {menuOptions[i].PadRight(15)} <   ║");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"║   {menuOptions[i].PadRight(15)}     ║");
                    }
                }
                Console.WriteLine("╚═════════════════════════════════╝");

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
                                // Inloggning
                                Console.WriteLine("Du valde Inloggning");
                                //Kod för inlogg
                                break;
                            case 1:
                                // Registrering
                                Console.WriteLine("Du valde Registrering");
                                //kod för registrering
                                break;
                            case 2:
                                // Avsluta programmet
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
