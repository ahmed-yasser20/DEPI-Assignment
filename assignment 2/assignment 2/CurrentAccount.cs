using System;

class CurrentAccount : BankAccount
{
    private decimal _overdraftLimit;

    public CurrentAccount(string fullName, decimal balance, decimal overdraftLimit)
        : base(fullName, balance)
    {
        _overdraftLimit = overdraftLimit;
    }

    public override void ShowAccountDetails()
    {
        base.ShowAccountDetails();
        Console.WriteLine("Overdraft Limit: " + _overdraftLimit);
    }

    public override decimal CalculateInterest()
    {
        return 0;
    }
}
