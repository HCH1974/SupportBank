namespace supportbank;

class Account
{
    public string Name { get; set; }
    public List<Transaction> TransactionsIn { get; set; }
    public List<Transaction> TransactionsOut { get; set; }

    public Account(string name)
    {
        Name = name;
        TransactionsIn = new List<Transaction>();
        TransactionsOut = new List<Transaction>();
    }

    public void AddTransactionIn(string transDate, Account accountFrom, Account accountTo, string description, decimal amount)
    {
        TransactionsIn.Add(new Transaction(transDate, accountFrom, accountTo, description, amount));
    }

    public void AddTransactionOut(string transDate, Account accountFrom, Account accountTo, string description, decimal amount)
    {
        TransactionsOut.Add(new Transaction(transDate, accountFrom, accountTo, description, amount));
    }
}