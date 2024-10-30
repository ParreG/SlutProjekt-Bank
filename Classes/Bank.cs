using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Bank
    {


        public void ShowMenu()
        {
            
            bool programAktivt = true;

           
            int menuSelected = 0;

             
            string[] menuOptions = new string[] { "Menyval1\t\t", "Menyval2\t\t", "Menyval3\t\t", "Menyval4\t\t", "Avsluta programmet\t" };

            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("Hej och välkommen till bankens meny.");
                Console.WriteLine("Du kan navigera med \" ⬇️\" och \" ⬆️\". \nTryck på \"Enter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        
                        Console.WriteLine("->" + menuOptions[i] + " <------");
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                
                var keyPressed = Console.ReadKey();

                
                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelected != menuOptions.Length - 1)
                {
                    menuSelected++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelected >= 1)
                {
                    menuSelected--;
                }

                
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

                    
                    switch (menuSelected)
                    {
                        
                        case 0:

                            //MENYVAL1();
                            break;
                        case 1:

                            //Menyval2();
                            break;
                        case 2:

                            //Menyval3();
                            break;
                        case 3:
                            //Avslutar programmet.
                            programAktivt = false;
                            break;
                    }
                }
            }
        }

    }
}
