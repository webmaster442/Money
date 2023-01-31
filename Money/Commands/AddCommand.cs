using System.Diagnostics.CodeAnalysis;

namespace Money.Commands
{
    internal sealed class AddCommand : Command<AddSettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public AddCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] AddSettings settings)
        {
            bool result = _writeOnlyData.TryInsert(settings.Ammount,
                                            settings.Text,
                                            settings.Date,
                                            settings.Category,
                                            out ulong id);

            if (!result)
            {
                return Ui.Error(Resources.ErrorCategoryDoesntExist, settings.Category);
            }

            Ui.Success(id);
            return Constants.Success;
        }
    }
}
