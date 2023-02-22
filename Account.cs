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

    public void ListAccount()
    {
        Console.WriteLine($"Account: {Name}");

        foreach (Transaction transaction in TransactionsIn)
        {
            Console.WriteLine($"In: {transaction.TransDate}, {transaction.AccountFrom.Name}, {transaction.Description}, £{transaction.Amount}");
        }
        foreach (Transaction transaction in TransactionsOut)
        {
            Console.WriteLine($"Out: {transaction.TransDate}, {transaction.AccountTo.Name}, {transaction.Description}, £{transaction.Amount}");
        }

    }
}