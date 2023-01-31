using System.Diagnostics.CodeAnalysis;

using Money.CommandsSettings;
using Money.Data;
using Money.Properties;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class CategoryRenameCommand : Command<CategoryRenameSettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public CategoryRenameCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] CategoryRenameSettings settings)
        {
            if (_writeOnlyData.TryRenameCategory(settings.OldCategoryName, settings.NewCategoryName))
            {
                Ui.Success(Resources.SuccessCategoryRename, settings.OldCategoryName, settings.NewCategoryName);
                return Constants.Success;
            }
            return Ui.Error(Resources.ErrorCategoryDoesntExist, settings.OldCategoryName);
        }
    }
}
