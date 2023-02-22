using supportbank;

Bank ourBank = new Bank();

var Lines = File.ReadLines("DodgyTransactions2015.csv");

List<string> transactions = new List<string>();

foreach (string line in Lines)
{
    transactions.Add(line);
}

transactions.RemoveAt(0);

for (int i = 0; i < transactions.Count; i++)
{
    string[] transactionArr = transactions[i].Split(",");

    try
    {
        DateTime.Parse(transactionArr[0]);
    }
    catch (FormatException)
    {
        Console.WriteLine($"Invalid date format on line {i}: '{transactionArr[0]}'. Date must be in dd/mm/yy.");
        continue;
    }

    try
    {
        Decimal.Parse(transactionArr[4]);
    }
    catch (FormatException)
    {
        Console.WriteLine($"Invalid amount on line {i}: '{transactionArr[4]}'. Amount must be in £x.xx format.");
        continue;
    }

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
