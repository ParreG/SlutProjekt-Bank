using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    using System;

    class BankAccount
    {
        public string Owner;// kontoägarens namn
        public decimal Balance;// kontosaldo
        //Konstruktor som skapar ett bankkonto med ägare och startsaldo
        public BankAccount(string owner, decimal balance)
        {
            Owner = owner; //Dehä är ägarens namn
            Balance = balance;//kontos startsaldo
        }
     
        // Det här är en method gör att överföra pengar från ett konto till en annan
    public void Transfer(BankAccount toAccount, decimal amount)
        {
            //Kollar om beloppet är positivt och om det finns tillräckligt med pengar i avsändarens konto 
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;//Minskar saldo från kontot som ska överföra
                toAccount.Balance += amount;//Ökar beloppet på mottagarens konto
                Console.WriteLine($"{Owner} skickade {amount} till {toAccount.Owner}");//Skriver ut att överföringen lyckats
            }
            else
            {
                //skrivs ut om överföringen misslyckats
                Console.WriteLine("Inte tillräckligt med pengar eller ogiltigt belopp.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            //Skapar ett konto för ali och ger han 1000
            var ali = new BankAccount("Ali", 1000);
            //Gör samma för Bart och ger han 500
            var bart = new BankAccount("Bart", 500);
            //Skriver ut både kontonas belopp
            Console.WriteLine($"Ali: {ali.Balance}, bart: {bart.Balance}");
            //Ali skickar 200 til bart
            ali.Transfer(bart, 200);
            //Skriver ut uppdaterade saldo efter överföringen för måde kontorna
            Console.WriteLine($"Ali: {ali.Balance}, bart: {bart.Balance}");
        }
    }
}
