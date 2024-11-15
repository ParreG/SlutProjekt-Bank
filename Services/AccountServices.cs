using SlutProjekt_Bank.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Services
{
    public static class AccountServices
    {

        public static List<Account>? Accounts { get; set; }
        private static readonly User _user;



        public static void AccountManager()
        {
            Console.WriteLine("Vad vill du öppna för konto?");
            string[] menuChoice = new string[] { "1. Vanlig konto", "2. Sparkonto" };

            for (int i = 0; i < menuChoice.Length; i++)
            {
                Console.WriteLine(menuChoice[i]);
            }
            Console.WriteLine();// extra utrymme!
            Console.WriteLine("Skriv in nummer på det valet du vill göra!");
            Console.Write("Ditt val: ");

            int choice;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");

                }
                else
                {
                    break;
                }
            }


            if (choice == 1)
            {
                Console.Write("Skriv in Kontonamn: ");
                var accountName = Console.ReadLine();
                Console.Write("Skriv in valuta: ");
                var currency = Console.ReadLine();
                Console.Write("Skriv in saldo: ");
                var initialBalance = decimal.Parse(Console.ReadLine()); //Denna kan inte hantera fel input än!

                CreateAccount(initialBalance, currency, accountName);

                displayAccounts();

            }
            else if (choice == 2)
            {
                SavingAccount(_user, 0, "Sek");
            }



        }

        public static void PersonalTransfer()
        {
            var userAccounts = Accounts.Where(Accounts => Accounts.AccountHolder == Security.CurrentUser).ToList();
            if (userAccounts == null || userAccounts.Count < 2)
            {
                Console.WriteLine("Finns inga konton att föra över pengar mellan!\n Vänligen skapa ett konto först!");
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

        public static Account CreateAccount(decimal initialBalance, string currency, string accountName)
        {
            var account = new Account(Security.CurrentUser, initialBalance, currency, accountName);

            if (Accounts == null)
            {
                Accounts = new List<Account>();
            }

            Accounts.Add(account);

            return account;
        }

        public static void displayAccounts()
        {
            if (Accounts == null || !Accounts.Any())
            {
                Console.WriteLine("Finns inga konton att visa.");
                return;
            }

            // Filter accounts for the current user
            var userAccounts = Accounts.Where(Accounts => Accounts.AccountHolder == Security.CurrentUser).ToList();
            if (!userAccounts.Any())
            {
                Console.WriteLine("Du har inga konton att visa.");
                return;
            }

            Console.WriteLine(); // för extra utrymme
            foreach (var account in userAccounts)
            {
                Console.WriteLine($"Kontonamn: {account.AccountName}\n" +
                                 $"Valuta: {account.Currency}\n" +
                                 $"Saldo: {account.Balance}\n");
            }
        }


        public static void Deposit()
        {
            var userAccounts = Accounts.Where(Accounts => Accounts.AccountHolder == Security.CurrentUser).ToList();
            if (userAccounts == null || userAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton!");
            }
            else
            {
                Console.WriteLine("Vilket konto vill du sätta in pengar på?");
                var account = userAccounts.FirstOrDefault(x => x.AccountName == Console.ReadLine());
                Console.WriteLine("Hur mycket pengar vill du sätta in?");
                decimal amount = decimal.Parse(Console.ReadLine());
                account.Balance += amount;
            }
        }

        public static void Withdraw()
        {
            var userAccounts = Accounts.Where(Accounts => Accounts.AccountHolder == Security.CurrentUser).ToList();
            if (userAccounts == null || userAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton");
            }
            else
            {
                Console.WriteLine("Vilket konto vill du ta ut pengar från?");
                var account = userAccounts.FirstOrDefault(x => x.AccountName == Console.ReadLine());
                Console.WriteLine("Hur mycket vill du ta ut?");
                decimal amount = decimal.Parse(Console.ReadLine());
                if (account.Balance >= amount)
                    account.Balance -= amount;
                Console.WriteLine($"Du har tagit ut {amount} från {account.AccountName}");
            }
        }

        public static bool Transfers(Account account1, Account account2, decimal amount)
        {
            if (account1.Balance >= amount)
            {
                account1.Balance -= amount;
                account2.Balance += amount;
                return true;
            }
            else
            {
                Console.WriteLine("Otillräckliga medel för överföring.");
                return false;
            }
        }

        public static void Loan(List<Account> accounts)
        {

            var userAccounts = Accounts.Where(Accounts => Accounts.AccountHolder == Security.CurrentUser).ToList();
            if (userAccounts == null || userAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton att sätta de inlånade pengarna i!\nVänligen öppna ett konto först!");
                return;
            }

            Console.WriteLine("Välj ett konto för att sätta in lånebeloppet i:");
            Console.WriteLine();
            for (int i = 0; i < userAccounts.Count; i++)
            {
                Console.WriteLine($"------- Konto nummer : {i + 1} -------\nKontonamn: {userAccounts[i].AccountName}, Saldo: {userAccounts[i].Balance} {userAccounts[i].Currency}\n\n");
            }

            int accountIndex;
            while (true)
            {
                Console.Write("Ange kontonummer: ");
                if (int.TryParse(Console.ReadLine(), out accountIndex) && accountIndex > 0 && accountIndex <= userAccounts.Count)
                {
                    break;
                }
                Console.WriteLine("Ogiltigt val, försök igen.");
            }

            Account selectedAccount = userAccounts[accountIndex - 1];

            decimal totalBalance = userAccounts.Sum(a => a.Balance);
            decimal maxLoanAmount = totalBalance * 5;
            decimal loanAmount;

            while (true)
            {
                Console.WriteLine($"\nDu kan maximalt låna {maxLoanAmount} {selectedAccount.Currency}. Hur mycket vill du låna?");

                if (!decimal.TryParse(Console.ReadLine(), out loanAmount) || loanAmount <= 0 || loanAmount > maxLoanAmount)
                {
                    Console.WriteLine("Ogiltigt belopp. Du kan bara låna 5x av det totala som du har i ditt konto!");
                    return;
                }
                else
                {
                    break;
                }
            }

            selectedAccount.Balance += loanAmount;
            Console.WriteLine($"Du har lånat {loanAmount} {selectedAccount.Currency}. Ränta: 5% per 60 sekunder.");
            Console.WriteLine($"Nytt saldo efter lånet: {selectedAccount.Balance:F2} {selectedAccount.Currency}");

            Thread loanInterest = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000);

                    decimal interest = loanAmount * 0.05m;
                    loanAmount += interest;

                    Console.WriteLine($"Löpande ränta på {interest} {selectedAccount.Currency} tillagt. Aktuellt lånebelopp: {loanAmount}{selectedAccount.Currency}.");
                }
            });

            loanInterest.Start();
        }

        public static Account SavingAccount(User user, decimal initialBalance, string currency)
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
                Account savingAccount = new Account(Security.CurrentUser, sbalance, currency, savingAccountName);

                Console.WriteLine($"Ditt sparkonto '{savingAccountName}' har skapats med ett startbelopp på {sbalance} {currency}.");


                if (Accounts == null)
                {
                    Accounts = new List<Account>();
                }


                Accounts.Add(savingAccount);


                Thread interest = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(30000); // 30 sekunder
                        savingAccount.Balance += (savingAccount.Balance * 0.024m); // Lägg till 2,4 % ränta
                        Console.WriteLine($"Ny ränta tillagd! Aktuellt saldo på '{savingAccount.AccountName}': {savingAccount.Balance:F2} {savingAccount.Currency}"); //avrundar till 2 decimaler
                    }
                });
                interest.Start();

                return savingAccount; // Returnera det skapade kontot
            }
            else
            {
                Console.WriteLine("Felaktigt belopp angivet. Kontot kunde inte skapas.");
                return null;
            }
        }


        public static void DisplayBalance(List<Account> Accounts)
        {
            foreach (Account account in Accounts)
            {
                Console.WriteLine($"Aktuellt saldo: {account.Balance}");
            }

        }
    }
}
