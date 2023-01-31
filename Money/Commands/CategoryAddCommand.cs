using System.Diagnostics.CodeAnalysis;

namespace Money.Commands
{
    internal sealed class CategoryAddCommand : AsyncCommand<CategorySettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public CategoryAddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context,
                                               [NotNull] CategorySettings settings)
        {
            var result = await _writeOnlyData.CreateCategoryAsync(settings.CategoryName)

            if (!result.success)
                return Ui.Error(Resources.ErrorCategoryAllreadyExists, settings.CategoryName);

            Ui.Success(result.id);

            return Constants.Success;
        }
    }
}
