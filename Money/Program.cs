using Microsoft.Extensions.DependencyInjection;

using Money;
using Money.Commands;
using Money.Data;
using Money.Data.DataAccess;
using Money.Properties;

using Spectre.Console.Cli;

ServiceCollection registrations = new ServiceCollection();
registrations.AddSingleton<IWriteOnlyData, WriteOnlyData>();
registrations.AddSingleton<IReadonlyData, ReadOnlyData>();

ITypeRegistrar registrar = new TypeRegistrar(registrations);

CommandApp app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("money");
    config.

    config
        .AddCommand<AddCommand>("add")
        .WithDescription(Resources.CmdAddDescription);

    config.AddBranch("category", category =>
    {
        category.SetDescription(Resources.CmdCategoryDescription);

        category
            .AddCommand<CategoryAddCommand>("add")
            .WithDescription(Resources.CmdCategoryAddDescription);
        category
            .AddCommand<CategoryListCommand>("list")
            .WithDescription(Resources.CmdCategoryListDescription);
    });
    config.AddBranch("export", export =>
    {
        export.SetDescription(Resources.CmdExportDescription);

        export
            .AddCommand<ExportExcelCommand>("excel")
            .WithDescription(Resources.CmdExportExcelDescription);
        export
            .AddCommand<ExportBackupCommand>("backup")
            .WithDescription(Resources.CmdExportBackupDescription);
    });
    config.AddBranch("import", import =>
    {
        import.SetDescription(Resources.CmdImportDescription);

        import
            .AddCommand<CreateExcelTemplateCommand>("createtemplate")
            .WithDescription(Resources.CmdImportTemplateDescription);

        import
            .AddCommand<ImportExcelCommand>("excel")
            .WithDescription(Resources.CmdImportExcelDescription);
        import
            .AddCommand<ImportBackupCommand>("backup")
            .WithDescription(Resources.CmdImportBackupDescription);
    });

    config
        .AddCommand<StatCommand>("stat")
        .WithDescription(Resources.CmdStatDescription);
});

return app.Run(args);