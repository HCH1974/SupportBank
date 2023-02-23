using supportbank;
using NLog;
using NLog.Config;
using NLog.Targets;

var config = new LoggingConfiguration();
var target = new FileTarget { FileName = @"C:\Training\support-bank\Logs\SupportBank${shortdate}.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
config.AddTarget("File Logger", target);
config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
LogManager.Configuration = config;

Bank ourBank = new Bank();

ourBank.ProcessInputFile();

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
