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
            if (Accounts == null)
            {
                Console.WriteLine("Finns inga konton att visa.");
                return;
            }
            else
            {
                foreach (Account account in Accounts)
                {
                    Console.WriteLine($"Kontonamn: {account.AccountName}, Valuta: {account.Currency}, Saldo: {account.Balance}");
                }
            }
        }

        public void Deposit()
        {
            if (Accounts == null || Accounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton");
            }
            else
            {
                Console.WriteLine("Vilket konto vill du sätta in pengar på?");
                var account = Accounts.FirstOrDefault(x => x.AccountName == Console.ReadLine());
                Console.WriteLine("Hur mycket pengar vill du sätta in?");
                decimal amount = decimal.Parse(Console.ReadLine());
                account.Balance += amount;
            }
        }

        public void Withdraw()
        {
            if (Accounts == null || Accounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton");
            }
            else
            {
                Console.WriteLine("Vilket konto vill du ta ut pengar från?");
                var account = Accounts.FirstOrDefault(x => x.AccountName == Console.ReadLine());
                Console.WriteLine("Hur mycket vill du ta ut?");
                decimal amount = decimal.Parse(Console.ReadLine());
                if (account.Balance >= amount)
                
                account.Balance -= amount;
                Console.WriteLine($"Du har tagit ut {amount} från {account.AccountName}");
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

        public void Loan(List<Account> accounts)
        {

            if (accounts == null || accounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton att sätta de inlånade pengarna i!\nVänligen öppna ett konto först!");
                return;
            }

            Console.WriteLine("Välj ett konto för att sätta in lånebeloppet i:");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Kontonamn: {accounts[i].AccountName}, Saldo: {accounts[i].Balance} {accounts[i].Currency}");
            }

            int accountIndex;
            while (true)
            {
                Console.Write("Ange kontonummer: ");
                if (int.TryParse(Console.ReadLine(), out accountIndex) && accountIndex > 0 && accountIndex <= accounts.Count)
                {
                    break;
                }
                Console.WriteLine("Ogiltigt val, försök igen.");
            }

            Account selectedAccount = accounts[accountIndex - 1];

            decimal totalBalance = accounts.Sum(a => a.Balance);
            decimal maxLoanAmount = totalBalance * 5;
            decimal loanAmount;

            while (true)
            {
                Console.WriteLine($"\nDu kan maximalt låna {maxLoanAmount} {selectedAccount.Currency}. Hur mycket vill du låna?");

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

            selectedAccount.Balance += loanAmount;
            Console.WriteLine($"Du har lånat {loanAmount} {selectedAccount.Currency}. Ränta: 5% per 60 sekunder.");
            Console.WriteLine($"Nytt saldo efter lånet: {selectedAccount.Balance} {selectedAccount.Currency}");

            Thread loanInterest = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000);

                    decimal interest = loanAmount * 0.05m;
                    loanAmount += interest;

                    Console.WriteLine($"Löpande ränta på {interest} {selectedAccount.Currency} tillagt. Aktuellt lånebelopp: {loanAmount} {selectedAccount.Currency}. Nytt saldo: {selectedAccount.Balance} {selectedAccount.Currency}");
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
