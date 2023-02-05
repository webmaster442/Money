[7mMoney[0m

A spimple console expense manager software written in C#. It uses EntityFramework to store data and for console output Spectre.Console.

[4;93mUsage[0m

[4;94mAdd[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mAdd a spending to the database[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money add [amount] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [amount]    Ammount of money spent[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help        Prints help information                                   [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -t, --text        A short text description of the spending                  [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -c, --category    Spending category                                         [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -d, --date        Spending time. If not set current date & time will be used[500@[0m

[4;94mFind[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mSearch spendings in the database[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money find [term] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [term]    Search term[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help         Prints help information                                     [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -r, --regex                                                                    [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -s, --startdate    Start date (optional)                                       [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -e, --enddate      End date (optional)                                         [500@[0m
[48;2;155;155;155;38;2;30;30;30m    -c, --category     Spending category. When set, only searches in given category[500@[0m

[4;94mCategory add[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mAdd a spending category[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money category add [category] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [category]    Category name[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mCategory list[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mList all spending categories[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money category list [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mCategory rename[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mRename a category[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money category rename [oldcategory] [newcategory] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [oldcategory]    Old category name[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [newcategory]    New category name[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mExport backup[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mExport data to backup format[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money export backup [file] [start] [end] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [file]     file name    [500@[0m
[48;2;155;155;155;38;2;30;30;30m    [start]    Start date (optional)[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [end]      End date (optional)[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mExport excel[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mExport data to excel xlsx file[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money export excel [file] [start] [end] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [file]     file name            [500@[0m
[48;2;155;155;155;38;2;30;30;30m    [start]    Start date (optional)[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [end]      End date (optional)  [500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mImport backup[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mImport data to money backup format[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money import backup [file] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [file]    file name[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mImport excel[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mImport data from excel xlsx file[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money import excel [file] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [file]    file name[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mImport createtemplate[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mCreate excel template for importing[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money import createtemplate [file] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [file]    file name[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help    Prints help information[500@[0m

[4;94mStat[0m

[48;2;155;155;155;38;2;30;30;30mDESCRIPTION:[500@[0m
[48;2;155;155;155;38;2;30;30;30mDisplay statistics about spendings[500@[0m
[48;2;155;155;155;38;2;30;30;30mUSAGE:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    money stat [start] [end] [OPTIONS][500@[0m
[48;2;155;155;155;38;2;30;30;30mARGUMENTS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    [start]    Start date   [500@[0m
[48;2;155;155;155;38;2;30;30;30m    [end]      End date date[500@[0m
[48;2;155;155;155;38;2;30;30;30mOPTIONS:[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -h, --help        Prints help information[500@[0m
[48;2;155;155;155;38;2;30;30;30m    -d, --detailed                           [500@[0m


