using supportbank;

Bank ourBank = new Bank();

var Lines = File.ReadLines("Transactions2014.csv");

List<string> transactions = new List<string>();

foreach (string line in Lines)
{
    transactions.Add(line);
}

transactions.RemoveAt(0);

foreach (string transaction in transactions)
{
    string[] transactionArr = transaction.Split(",");

    Account accountFrom = ourBank.FindOrCreateAccount(transactionArr[1]);

    Account accountTo = ourBank.FindOrCreateAccount(transactionArr[2]);

    accountFrom.AddTransactionOut(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));

    accountTo.AddTransactionIn(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
}

Console.Write("Would you like to (1) ListAll  or (2) List[Account]: ");
string choice = Console.ReadLine()!;

if (choice == "1")
{
    ourBank.ListAll();
}

if (choice == "2")
{
    Console.Write("Please enter account name: ");
    string accChoice = Console.ReadLine()!;
    ourBank.FindOrCreateAccount(accChoice).ListAccount();
}
