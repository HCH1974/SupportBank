using supportbank;

Bank ourBank = new Bank();

var Lines = File.ReadLines("Transactions2014.csv");

List<string> transactions = new List<string>();

foreach (string line in Lines)
{
    transactions.Add(line);
}

foreach (string transaction in transactions)
{
    string[] transactionArr = transaction.Split(",");

    Account accountFrom = ourBank.FindOrCreateAccount(transactionArr[1]);

    Account accountTo = ourBank.FindOrCreateAccount(transactionArr[2]);

    accountFrom.AddTransactionOut(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));

    accountTo.AddTransactionIn(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
}







// string transact1 = "01/01/2014,Jon A,Sarah T,Pokemon Training,7.8";

// string[] transact1arr = transact1.Split(",");


// ourBank.AddAccount(new Account(transact1arr[1], ourBank));

// ourBank.AddAccount(new Account(transact1arr[2], ourBank));

// ourBank.AccountList[0].AddTransactionIn(transact1arr[0], jonAccount, sarahAccount, transact1arr[3], Decimal.Parse(transact1arr[4]));
// jonAccount.AddTransactionOut(transact1arr[0], jonAccount, sarahAccount, transact1arr[3], Decimal.Parse(transact1arr[4]));