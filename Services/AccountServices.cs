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
        public void Transfer()
        {
            CreateAccount( 100, "sek", "konto1");
            CreateAccount( 200, "sek", "konto2");
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
        public void SavingAccount(Client client, decimal balance, string currency)
        {
            Console.WriteLine("Vad härligt att du vill öppna ett sparkonto.");
            Console.WriteLine("Vad ska sparkontot heta: ");
            string savingAccountName = Console.ReadLine();
            Account savingAccount = new Account(balance, currency, savingAccountName);


            // Hur mycket ska överföras till kontot? 
            // från vilket konto?

            //Måste ta med TransferMoney(); ?
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
