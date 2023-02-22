namespace supportbank;

class Bank
{
    public List<Account> AccountList { get; set; }

    public Bank()
    {
        AccountList = new List<Account>();
    }

    // public void AddAccount(Account account)
    // {
    //     AccountList.Add(account);
    // }

    // public bool CheckAccount(string accountname)
    // {
    //     foreach (Account item in AccountList)
    //     {
    //         if (item.Name == accountname) return true;
    //     }
    //     return false;
    // }
    

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
}