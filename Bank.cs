using NLog;
using Newtonsoft;
using Newtonsoft.Json;

namespace supportbank;


class Bank
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public List<Account> AccountList { get; set; }

    public Bank()
    {
        AccountList = new List<Account>();
        Logger.Info("New bank successfully created.");
    }
    public void ProcessInputFile()
    {
        string inputFile;
        List<string> transactions = new List<string>();
        var transactionJsonList = new List<Transactionjson>();
        while (!transactions.Any() && !transactionJsonList.Any())
        {
            try
            {
                Console.Write("Enter the name of your input data file: ");
                inputFile = Console.ReadLine()!;
                string fileExt = Path.GetExtension(inputFile);
                if (fileExt == ".csv")
                {
                    var Lines = File.ReadLines(inputFile);
                    foreach (string line in Lines)
                    {
                        transactions.Add(line);
                    }
                    transactions.RemoveAt(0);
                }
                else if (fileExt == ".json")
                {
                    transactionJsonList = (JsonConvert.DeserializeObject<List<Transactionjson>>(File.ReadAllText(inputFile)))!;
                }
                Logger.Info($"User entered {inputFile}, this was successfully read.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Sorry that file couldn't be found, please try again: ");
                Logger.Warn($"The user entered an incorrect file path.");
            }
        }

        bool anyErrors = false;

        if (transactions.Any())
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                string[] transactionArr = transactions[i].Split(",");

                if (!CheckDataFormat(transactionArr[0], transactionArr[4], i))
                {
                    anyErrors = true;
                    continue;
                }

                Account accountFrom = FindOrCreateAccount(transactionArr[1]);
                Account accountTo = FindOrCreateAccount(transactionArr[2]);

                accountFrom.AddTransactionOut(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
                accountTo.AddTransactionIn(transactionArr[0], accountFrom, accountTo, transactionArr[3], Decimal.Parse(transactionArr[4]));
            }
        }
        else if (transactionJsonList.Any())
        {
            for (int i = 0; i < transactionJsonList.Count; i++)
            {
                if (!CheckDataFormat(transactionJsonList[i].Date, transactionJsonList[i].Amount.ToString(), i))
                {
                    anyErrors = true;
                    continue;
                }

                Account accountFrom = FindOrCreateAccount(transactionJsonList[i].FromAccount);
                Account accountTo = FindOrCreateAccount(transactionJsonList[i].ToAccount);

                accountFrom.AddTransactionOut(transactionJsonList[i].Date, accountFrom, accountTo, transactionJsonList[i].Narrative, transactionJsonList[i].Amount);
                accountTo.AddTransactionIn(transactionJsonList[i].Date, accountFrom, accountTo, transactionJsonList[i].Narrative, transactionJsonList[i].Amount);   
            }
        }

        if (anyErrors)
        {
            throw new FormatException("Errors as above.");
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
        Logger.Info("Report of All Accounts complete.");
    }

    public bool CheckDataFormat(string dateString, string amountString, int i)
    {
        try
        {
            DateTime.Parse(dateString);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Invalid date format on line {i + 2}: '{dateString}'. Date must be in dd/mm/yy.");
            Logger.Error($"Invalid date format on line {i + 2}: '{dateString}'.\nError: {ex}");
            return false;
        }
        try
        {
            Decimal.Parse(amountString);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Invalid amount on line {i + 2}: '{amountString}'. Amount must be in £x.xx format.");
            Logger.Error($"Invalid amount on line {i + 2}: '{amountString}'.\nError: {ex}");
            return false;
        }
        return true;
    }
}