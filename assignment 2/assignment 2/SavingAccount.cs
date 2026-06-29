using System;

class SavingAccount : BankAccount
{
    private decimal _interestRate;

    public SavingAccount(string fullName, decimal balance, decimal interestRate)
        : base(fullName, balance)
    {
        _interestRate = interestRate;
    }

    public override void ShowAccountDetails()
    {
        base.ShowAccountDetails();
        Console.WriteLine("Interest Rate: " + _interestRate);
    }

    public override decimal CalculateInterest()
    {
        return _balance * _interestRate;
    }
}
