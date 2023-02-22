namespace supportbank;

class Bank
{
    public List<Account> AccountList { get; set; }

    public Bank()
    {
        AccountList = new List<Account>();
    }
    public void ProcessInputFile(string inputFileName)
    {
        var Lines = File.ReadLines(inputFileName);
        List<string> transactions = new List<string>();

        foreach (string line in Lines)
        {
            transactions.Add(line);
        }
        // First line has column headings
        transactions.RemoveAt(0);

        for (int i = 0; i < transactions.Count; i++)
        {
            string[] transactionArr = transactions[i].Split(",");

            if (!CheckDataFormat(transactionArr[0] , transactionArr[4] , i)) continue;

            Account accountFrom = FindOrCreateAccount(transactionArr[1]);
            Account accountTo = FindOrCreateAccount(transactionArr[2]);

            accountFrom.AddTransactionOut(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
            accountTo.AddTransactionIn(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
        }
    }   
   public Account FindOrCreateAccount(string accountname)
    {
        foreach (Account account in AccountList)
        {
            if (account.Name == accountname) return account;
        }
        Account newAccount = new Account(accountname);
        AccountList.Add(newAccount);
        return newAccount;
    }
    public void ListAll()
    {
        foreach (Account account in AccountList)
        {
            decimal amountIn = 0m;
            foreach (Transaction transaction in account.TransactionsIn)
            {
                amountIn += transaction.Amount;
            }

            decimal amountOut = 0m;
            foreach (Transaction transaction in account.TransactionsOut)
            {
                amountOut += transaction.Amount;
            }
            Console.WriteLine($"Account: {account.Name}\n\tAmount owing: £{amountIn}\n\tAmount owed: £{amountOut}");
        }
    }

     public bool CheckDataFormat(string dateString , string amountString, int i)
     {
        try
        {
            DateTime.Parse(dateString);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Invalid date format on line {i + 2}: '{dateString}'. Date must be in dd/mm/yy.");
            return false;
        }
        try
        {
            Decimal.Parse(amountString);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Invalid amount on line {i + 2}: '{amountString}'. Amount must be in £x.xx format.");
            return false;
        }
        return true;
    }
}