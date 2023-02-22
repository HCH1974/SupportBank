namespace supportbank;

class Transaction
{
    public string TransDate { get; set; }
    public Account AccountFrom { get; set; }
    public Account AccountTo { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public Transaction(string transDate, Account accountFrom, Account accountTo, string description, decimal amount)
    {
        TransDate = transDate;
        AccountFrom = accountFrom;
        AccountTo = accountTo;
        Description = description;
        Amount = amount;
    }
}