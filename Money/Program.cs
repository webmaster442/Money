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
    config.AddCommand<AddCommand>("add");
    config.AddBranch("category", category =>
    {
        category.AddCommand<CategoryAddCommand>("add");
        category.AddCommand<CategoryListCommand>("list");
    });
    config.AddBranch("export", export =>
    {
        export.AddCommand<ExportExcelCommand>("excel");
    });
    config.AddCommand<StatCommand>("stat");
}); 

return app.Run(args);