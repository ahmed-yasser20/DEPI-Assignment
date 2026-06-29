using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SavingAccount sa = new SavingAccount("Ahmed Yasser", 10000, 0.05m);
            CurrentAccount ca = new CurrentAccount("Ali Mohamed", 5000, 2000);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(sa);
            accounts.Add(ca);

            foreach (BankAccount acc in accounts)
            {
                acc.ShowAccountDetails();
                Console.WriteLine("Calculated Interest: " + acc.CalculateInterest());
                Console.WriteLine("---------------------------");
            }
        }
    }
}