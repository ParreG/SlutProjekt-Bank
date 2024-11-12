using SlutProjekt_Bank.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Services
{
    public class AccountServices
    {

        public List<Account>? Accounts { get; set; }

        public void AccountMeny()
        {
            //Ska andra menyn för inloggade användare gå här?
        }


        public void AccountManager()
        {
            Console.WriteLine("Skriv in Kontonamn");
            var accountName = Console.ReadLine();
            Console.WriteLine("Skriv in valuta");
            var currency = Console.ReadLine();
            Console.WriteLine("Skriv in saldo");
            var initialBalance = decimal.Parse(Console.ReadLine());

            CreateAccount(initialBalance, currency, accountName);

            displayAccounts();
            DisplayBalance(Accounts);

        }

        //public void Transfer(BankAccount toAccount, decimal amount)
        //{

        //    if (amount > 0 && amount <= Balance)
        //    {
        //        Balance -= amount;
        //        toAccount.Balance += amount;
        //        Console.WriteLine($"{Owner} skickade {amount} till {toAccount.Owner}");
        //    }
        //    else
        //    {

        //        Console.WriteLine("Inte tillräckligt med pengar eller ogiltigt belopp.");
        //    }
        //}

        public void PersonalTransfer()
        {
            CreateAccount(100, "sek", "konto1");
            CreateAccount(200, "sek", "konto2");
            if (Accounts == null || Accounts.Count < 2)
            {
                Console.WriteLine("Finns inga konton att föra över pengar mellan");
                return;
            }

            displayAccounts();

            Console.WriteLine("Från vilket konto vill du föra över pengar?");
            var fromAccountName = Console.ReadLine();
            var fromAccount = Accounts.FirstOrDefault(x => x.AccountName == fromAccountName);

            if (fromAccount == null)
            {
                Console.WriteLine("Kontot hittades inte");
                return;
            }

            Console.WriteLine("Till vilket konto vill du föra över pengar?");
            var toAccountName = Console.ReadLine();
            var toAccount = Accounts.FirstOrDefault(x => x.AccountName == toAccountName);

            if (toAccount == null)
            {
                Console.WriteLine("Kontot hittades inte");
                return;
            }

            if (fromAccount == toAccount)
            {
                Console.WriteLine("Går inte att föra över pengar till samma konto");
                return;
            }

            Console.WriteLine("Hur mycket pengar vill du föra över?");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Skriv in ett giltigt belopp");
                return;
            }
            if (Transfers(fromAccount, toAccount, amount))
            {
                Console.WriteLine($"Du förde över {amount} {fromAccount.Currency} från {fromAccount.AccountName} till {toAccount.AccountName}");
                DisplayBalance(Accounts);
            }


        }

        public Account CreateAccount(decimal initialBalance, string currency, string accountName)
        {
            var account = new Account(initialBalance, currency, accountName);

            if (Accounts == null)
            {
                Accounts = new List<Account>();
            }

            Accounts.Add(account);

            return account;
        }

        public void displayAccounts()
        {
            foreach (Account account in Accounts)
            {
                Console.WriteLine($"Kontonamn: {account.AccountName}, Valuta: {account.Currency}, Saldo: {account.Balance}");
            }
        }

        public void Deposit(Account account, decimal amount)
        {
            account.Balance += amount;
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
            }
            else
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot");
            }
        }

        public bool Transfers(Account account1, Account account2, decimal amount)
        {
            if (account1.Balance >= amount)
            {
                account1.Balance -= amount;
                account2.Balance += amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds for transfer.");
                return false;
            }
        }

        public void Loan(Account account)
        {
             /*
             Lån funktion made by Parre. Ränta på 5% var 60 sekunder. Vi ska bli rika XD
             Har ej testat den men hoppas den funkar!
             */
            decimal totalBalance = Accounts.Sum(a => a.Balance);
            decimal maxLoanAmount = totalBalance * 5;
            decimal loanAmount;

            while (true)
            {
                Console.WriteLine($"\nDu kan maximalt låna {maxLoanAmount} {account.Currency}. Hur mycket vill du låna?");

                if (!decimal.TryParse(Console.ReadLine(), out loanAmount) || loanAmount <= 0 || loanAmount > maxLoanAmount)
                {
                    Console.WriteLine("Ogiltigt belopp eller överstiger maximalt lånebelopp.");
                    return;

                }
                else
                {
                    break;

                }

            }
            
            account.Balance += loanAmount;
            Console.WriteLine($"Du har lånat {loanAmount} {account.Currency}. Ränta: 5% per 60 sekunder.");
            Console.WriteLine($"Nytt saldo efter lånet: {account.Balance} {account.Currency}");

            Thread loanInterest = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000); 

                    decimal interest = loanAmount * 0.05m;
                    loanAmount += interest;
                    account.Balance -= interest;

                    Console.WriteLine($"Löpande ränta på {interest} {account.Currency} tillagt. Aktuellt lånebelopp: {loanAmount} {account.Currency}. Nytt saldo: {account.Balance} {account.Currency}");
                }
            });

            loanInterest.Start();
        }


        public static void SavingAccount(User user, decimal initialBalance, string currency)
        {
            Console.WriteLine(); //för extra utrymme
            Console.WriteLine("Vad härligt att du vill öppna ett sparkonto.");
            Console.WriteLine("Hos oss får du 2.4% ränta på dina pengar per 30 sekunder.");

            Console.Write("Vad ska sparkontot heta: ");
            string savingAccountName = Console.ReadLine();

            Console.WriteLine("Hur mycket vill du sätta in som startbelopp?");
            decimal sbalance;

            if (decimal.TryParse(Console.ReadLine(), out sbalance) && sbalance >= 0)
            {
                Account savingAccount = new Account(sbalance, currency, savingAccountName);

                Console.WriteLine($"Ditt sparkonto '{savingAccountName}' har skapats med ett startbelopp på {sbalance}{currency}.");

                //Skapar en thread som alltid kommer gå i bakgrunden och ge räntan till kunden. 
                Thread interest = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(30000);
                        sbalance += (sbalance * 0.024m);
                    }

                });
                interest.Start();

            }
            else
            {
                Console.WriteLine("Felaktigt belopp angivet. Kontot kunde inte skapas.");
            }
        }

        public void DisplayBalance(List<Account> Accounts)
        {
            foreach (Account account in Accounts)
            {
                Console.WriteLine($"Current balance: {account.Balance}");
            }

        }
    }
}
