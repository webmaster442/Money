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
            ulong id = _writeOnlyData.Insert(settings.Ammount,
                                            settings.Text,
                                            settings.Date);
            Ui.Inserted(id);
            return Constants.Success;
        }
    }
}
