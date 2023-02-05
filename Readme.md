# Money

A spimple console expense manager software written in C#. It uses EntityFramework to store data and for console output Spectre.Console.

## Usage

### Add

```
DESCRIPTION:
Add a spending to the database

USAGE:
    money add [amount] [OPTIONS]

ARGUMENTS:
    [amount]    Ammount of money spent

OPTIONS:
    -h, --help        Prints help information                                   
    -t, --text        A short text description of the spending                  
    -c, --category    Spending category                                         
    -d, --date        Spending time. If not set current date & time will be used

```

### Find

```
DESCRIPTION:
Search spendings in the database

USAGE:
    money find [term] [OPTIONS]

ARGUMENTS:
    [term]    Search term

OPTIONS:
    -h, --help         Prints help information                                     
    -r, --regex                                                                    
    -s, --startdate    Start date (optional)                                       
    -e, --enddate      End date (optional)                                         
    -c, --category     Spending category. When set, only searches in given category
```

### Category add

```
DESCRIPTION:
Add a spending category

USAGE:
    money category add [category] [OPTIONS]

ARGUMENTS:
    [category]    Category name

OPTIONS:
    -h, --help    Prints help information

```

### Category list

```
DESCRIPTION:
List all spending categories

USAGE:
    money category list [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

### Category rename

```
DESCRIPTION:
Rename a category

USAGE:
    money category rename [oldcategory] [newcategory] [OPTIONS]

ARGUMENTS:
    [oldcategory]    Old category name
    [newcategory]    New category name

OPTIONS:
    -h, --help    Prints help information
```

### Export backup

```
DESCRIPTION:
Export data to backup format

USAGE:
    money export backup [file] [start] [end] [OPTIONS]

ARGUMENTS:
    [file]     file name    
    [start]    Start date (optional)
    [end]      End date (optional)

OPTIONS:
    -h, --help    Prints help information
```

### Export excel

```
DESCRIPTION:
Export data to excel xlsx file

USAGE:
    money export excel [file] [start] [end] [OPTIONS]

ARGUMENTS:
    [file]     file name            
    [start]    Start date (optional)
    [end]      End date (optional)  

OPTIONS:
    -h, --help    Prints help information
```

### Import backup

```
DESCRIPTION:
Import data to money backup format

USAGE:
    money import backup [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information

```

### Import excel

```
DESCRIPTION:
Import data from excel xlsx file

USAGE:
    money import excel [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information
```

### Import createtemplate

```
DESCRIPTION:
Create excel template for importing

USAGE:
    money import createtemplate [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information
```

### Stat

```
DESCRIPTION:
Display statistics about spendings

USAGE:
    money stat [start] [end] [OPTIONS]

ARGUMENTS:
    [start]    Start date   
    [end]      End date date

OPTIONS:
    -h, --help        Prints help information
    -d, --detailed                           

```
