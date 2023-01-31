using System.Diagnostics.CodeAnalysis;

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
