using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Money.CommandsSettings;
using Money.Data;
using Money.Data.Serialization;
using Money.Extensions;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class ImportCommand : Command<ImportSettings>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ImportSettings settings)
        {
            try
            {
                using (var fileStream = File.OpenRead(settings.FileName))
                {
                    using (var zip = new GZipStream(fileStream, CompressionMode.Decompress, true))
                    {
                        var data = zip.ReadJson<List<SerializableSpending>>();
                        _writeOnlyData.Import(data);
                        Ui.ImportSuccessfull(data.Count, settings.FileName);
                    }
                }
                return Constants.Success;
            }
            catch (IOException ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
