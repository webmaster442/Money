# Money

A spimple console expense manager software written in C#. It uses EntityFramework to store data and for console output Spectre.Console.

## Usage



### Add
```
Money add [amount] [OPTIONS]

ARGUMENTS:
    [amount]    Ammount of money spent

OPTIONS:
    -h, --help        Prints help information
    -t, --text        A short text description of the spending
    -c, --category    Spending category
    -d, --date        Spending time. If not set current date & time will be used
```