using supportbank;

Bank ourBank = new Bank();

Console.Write("Enter the name of your input data file: ");
string inputFile = Console.ReadLine()!;

ourBank.ProcessInputFile(inputFile);

Console.Write("Would you like to (1) ListAll  or (2) List[Account]: ");
string choice = Console.ReadLine()!;

if (choice == "1")
{
    ourBank.ListAll();
}
else if (choice == "2")
{
    Console.Write("Please enter account name: ");
    string accChoice = Console.ReadLine()!;
    
    ourBank.FindOrCreateAccount(accChoice).ListAccount();
}
