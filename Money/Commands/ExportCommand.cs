using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Money.CommandsSettings;
using Money.Data;
using Money.Extensions;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class ExportCommand : Command<ExportSettings>
    {
        private readonly IReadonlyData _readonlyData;

        public ExportCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ExportSettings settings)
        {
            try
            {
                using (var fileStream = File.Create(settings.FileName))
                {
                    using (var zip = new GZipStream(fileStream, CompressionLevel.SmallestSize, true))
                    {
                        var data = _readonlyData.Export();
                        zip.WriteJson(data);
                        Ui.ExportSuccessfull(data.Count, settings.FileName);
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
