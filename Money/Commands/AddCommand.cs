using System.Diagnostics.CodeAnalysis;

using Money.CommandsSettings;
using Money.Data;

using Spectre.Console.Cli;

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
                return Ui.Error($"Category doesn't exist: {settings.Category}.\r\n" +
                                " Create it first with the category add command");
            }

            Ui.Success(id);
            return Constants.Success;
        }
    }
}
