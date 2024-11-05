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
            
            //En flagga som håller koll på om programmet är aktivt. Denna hjälper mig att avsluta programmet ganska direkt om programavslut knappen trycks i menyn. 
            bool programAktivt = true;

            //Variabel för att hålla reda på valt menyval.
            int menuSelected = 0;

            
            //Lägger till menyval i en array.Samtigit lägger jag en tappsteg på de olika valen bara för desgin mässig. 
            string[] menuOptions = new string[] { "Inloggning\t\t", "Registrering\t\t", "Avsluta programmet\t" };

            while (programAktivt)
            {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("Hej och välkommen till bankens meny.");
                Console.WriteLine("Du kan navigera med \" ⬇️\" och \" ⬆️\". \nTryck på \"Ënter\" när du vill välja den menyn du är nöjd med.");
                Console.WriteLine();

                //Loopar igenom menyvalen och markerar det valda alternativet. Detta gör att jag inte behöver repetera samma del flera gånger. 
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == menuSelected)
                    {
                        //För att menyn ska synas bättre la jag till en pil innan menyn också
                        Console.WriteLine("->" + menuOptions[i] + " <------");
                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                //Läser in användarens tangenttryck.
                var keyPressed = Console.ReadKey();

                //Börjar en If sats. Denna tillåter användaren att scrolla genom menyn med pilen ner eller upp knappen.
                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelected != menuOptions.Length - 1)
                {
                    menuSelected++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelected >= 1)
                {
                    menuSelected--;
                }

                //Om användaren trycker på ENTER så väljs det alternativet i menyn. 
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

                    //Beroende på användarens val i menyn, tar programmet upp olika funktioner.
                    switch (menuSelected)
                    {
                        //Börjar på 0 då Arrayen börjar på 0.
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
                            
                            break;
                        case 4:
                            //Avslutar programmet.
                            programAktivt = false;
                            break;
                    }
                }
            }
        }

    }
}
