using System.Diagnostics.CodeAnalysis;

using Money.CommandsSettings;
using Money.Data;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class CategoryAddCommand : Command<CategorySettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public CategoryAddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] CategorySettings settings)
        {
            if (!_writeOnlyData.TryCreateCategory(settings.CategoryName, out ulong Id))
                return Ui.Error($"Can't create category {settings.CategoryName}. It allredy exists");

            Ui.Inserted(Id);

            return Constants.Success;
        }
    }
}
