using System.Diagnostics.CodeAnalysis;

using Money.CommandsSettings;
using Money.Data;
using Money.Properties;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class CategoryAddCommand : Command<CategorySettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public CategoryAddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] CategorySettings settings)
        {
            if (!_writeOnlyData.TryCreateCategory(settings.CategoryName, out ulong id))
                return Ui.Error(Resources.ErrorCategoryAllreadyExists, settings.CategoryName);

            Ui.Success(id);

            return Constants.Success;
        }
    }
}
