using Microsoft.Extensions.DependencyInjection;

using Money;
using Money.Commands;
using Money.Data.DataAccess;
using Money.Infrastructure;

ServiceCollection registrations = new();
registrations.AddSingleton<ISettingsManager, SettingsManager>();
registrations.AddSingleton<IDatabaseFileLocator, DatabaseFileLocator>();
registrations.AddSingleton<IWriteOnlyData, WriteOnlyData>();
registrations.AddSingleton<IReadonlyData, ReadOnlyData>();

ITypeRegistrar registrar = new TypeRegistrar(registrations);

CommandApp app = new CommandApp(registrar);

app.SetDefaultCommand<ManualCommand>();

app.Configure(config =>
{
    config.SetApplicationName("money");

    config
        .AddCommand<SettingsCommand>("settings")
        .WithDescription(Resources.CmdSettingsDescription);

    config
        .AddCommand<GenTestDataCommand>("gentestdata")
        .IsHidden();

    config
        .AddCommand<AddCommand>("add")
        .WithDescription(Resources.CmdAddDescription);

    config
        .AddCommand<FindCommand>("find")
        .WithDescription(Resources.CmdFindDescription);

    config
        .AddCommand<StatCommand>("stat")
        .WithDescription(Resources.CmdStatDescription);

    config.AddBranch("category", category =>
    {
        category.SetDescription(Resources.CmdCategoryDescription);

        category
            .AddCommand<CategoryAddCommand>("add")
            .WithDescription(Resources.CmdCategoryAddDescription);
        category
            .AddCommand<CategoryListCommand>("list")
            .WithDescription(Resources.CmdCategoryListDescription);
        category
            .AddCommand<CategoryRenameCommand>("rename")
            .WithDescription(Resources.CmdCategoryRenameDescription);
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

        export
            .AddCommand<ExportReportCommand>("report")
            .WithDescription(Resources.CmdExportReportDescription);

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
    config.AddBranch("sync", sync =>
    {
        sync.SetDescription(Resources.CmdSyncDescription);

        sync
            .AddCommand<SyncPushCommand>("push")
            .WithDescription(Resources.CmdSyncPushDescription);
    });
});

await app.RunAsync(args);