using Microsoft.Extensions.DependencyInjection;

using Money;
using Money.Commands;
using Money.Data;
using Money.Data.DataAccess;

using Spectre.Console.Cli;

ServiceCollection registrations = new ServiceCollection();
registrations.AddSingleton<IWriteOnlyData, WriteOnlyData>();
registrations.AddSingleton<IReadonlyData, ReadOnlyData>();

ITypeRegistrar registrar = new TypeRegistrar(registrations);

CommandApp app = new CommandApp(registrar);
app.Configure(config =>
{
    config
        .AddCommand<AddCommand>("add")
        .WithDescription("Add a spending to the database");

    config.AddBranch("category", category =>
    {
        category
            .AddCommand<CategoryAddCommand>("add")
            .WithDescription("Add a spending category");
        category
            .AddCommand<CategoryListCommand>("list")
            .WithDescription("List all spending categories");
    });
    config.AddBranch("export", export =>
    {
        export
            .AddCommand<ExportExcelCommand>("excel")
            .WithDescription("Export data to excel xlsx file");
        export
            .AddCommand<ExportBackupCommand>("backup")
            .WithDescription("Export data to money backup format");
    });
    config.AddBranch("import", import =>
    {
        import
            .AddCommand<CreateExcelTemplateCommand>("createtemplate")
            .WithDescription("Create excel template");

        import
            .AddCommand<ImportExcelCommand>("excel")
            .WithDescription("Import data from excel xlsx file");
        import
            .AddCommand<ImportBackupCommand>("backup")
            .WithDescription("Import data to money backup format");
    });

    config
        .AddCommand<StatCommand>("stat")
        .WithDescription("Display statistics about spendings");
});

return app.Run(args);