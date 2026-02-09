using System;

class BankAccount
{
    protected string _fullName;
    protected decimal _balance;

    public BankAccount(string fullName, decimal balance)
    {
        _fullName = fullName;
        _balance = balance;
    }

    public virtual void ShowAccountDetails()
    {
        Console.WriteLine("Account Holder: " + _fullName);
        Console.WriteLine("Balance: " + _balance);
    }

    public virtual decimal CalculateInterest()
    {
        return 0;
    }
}
