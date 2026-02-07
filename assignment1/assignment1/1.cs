
class BankAccount
{
    public const string BankCode = "BNK001";
    public readonly DateTime CreatedDate;

    private int _accountNumber;
    private string _fullName;
    private string _nationalID;
    private string _phoneNumber;
    private string _address;
    private decimal _balance;

    
    public string FullName
    {
        get
        {
            return _fullName;
        }
        set
        {
            if (value != null && value != "")
            {
                _fullName = value;
            }
            else
            {
                Console.WriteLine("Full Name cannot be empty");
            }
        }
    }

    public string NationalID
    {
        get
        {
            return _nationalID;
        }
        set
        {
            if (IsValidNationalID(value))
            {
                _nationalID = value;
            }
            else
            {
                Console.WriteLine("National ID must be exactly 14 digits");
            }
        }
    }

    public string PhoneNumber
    {
        get
        {
            return _phoneNumber;
        }
        set
        {
            if (IsValidPhoneNumber(value))
            {
                _phoneNumber = value;
            }
            else
            {
                Console.WriteLine("Phone number must start with 01 and be 11 digits");
            }
        }
    }

    public string Address
    {
        get
        {
            return _address;
        }
        set
        {
            _address = value; 
        }
    }

    public decimal Balance
    {
        get
        {
            return _balance;
        }
        set
        {
            if (value >= 0)
            {
                _balance = value;
            }
            else
            {
                Console.WriteLine("Balance cannot be negative");
            }
        }
    }

   
    public BankAccount()
    {
        CreatedDate = DateTime.Now;
        _accountNumber = 0;
        _fullName = "Unknown";
        _nationalID = "00000000000000";
        _phoneNumber = "01000000000";
        _address = "mounofia";
        _balance = 0;
    }

    
    public BankAccount(string fullName, string nationalID, string phone, string address, decimal balance)
    {
        CreatedDate = DateTime.Now;
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phone;
        Address = address;
        Balance = balance;
    }

    
    public BankAccount(string fullName, string nationalID, string phone, string address)
    {
        CreatedDate = DateTime.Now;
        FullName = fullName;
        NationalID = nationalID;
        PhoneNumber = phone;
        Address = address;
        Balance = 0;
    }

   
    public void ShowAccountDetails()
    {
        Console.WriteLine("===== Account Details =====");
        Console.WriteLine("Bank Code: " + BankCode);
        Console.WriteLine("Full Name: " + FullName);
        Console.WriteLine("National ID: " + NationalID);
        Console.WriteLine("Phone Number: " + PhoneNumber);
        Console.WriteLine("Address: " + Address);
        Console.WriteLine("Balance: " + Balance);
        Console.WriteLine("Created Date: " + CreatedDate);
        Console.WriteLine("===========================");
    }

    public bool IsValidNationalID(string nationalID)
    {
        if (nationalID.Length == 14)
        {
            long temp;
            if (long.TryParse(nationalID, out temp))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsValidPhoneNumber(string phone)
    {
        if (phone.StartsWith("01") && phone.Length == 11)
        {
            return true;
        }
        return false;
    }
}
