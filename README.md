# Support Bank

This is a console application written in C#. The data from an input file (of type csv, json or xml) is validated and a report is produced.

## Setup

To execute this program, run the command dotnet run. You will need to enter the input datacd su  file as one of the following:

Transactions2012.xml
Transactions2013.json
Transactions2014.csv
DodgyTransactions2015.csv

If the data is valid, one of two reports can be run over it:

1. List all the accounts with the amount owed and the amount owing
2. List the amounts coming in and out of one account.
