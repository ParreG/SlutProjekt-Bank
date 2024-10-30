using SlutProjekt_Bank.Classes;

namespace SlutProjekt_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Bank bank = new Bank();

            bank.ShowMenu();
        }
    }
}
