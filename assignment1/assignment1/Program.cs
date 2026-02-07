namespace assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount acc1 = new BankAccount("Ahmed Yasser", "12345678901234", "01012345678", "Cairo", 25000);

            BankAccount acc2 = new BankAccount("Aly el-abassy", "98765432109876", "01123456789", "Giza");

            acc1.ShowAccountDetails();
            acc2.ShowAccountDetails();
        }

    }
}


