namespace supportbank;

class Bank
{
    public List<Account> AccountList { get; set; }

    public Bank()
    {
        AccountList = new List<Account>();
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
}