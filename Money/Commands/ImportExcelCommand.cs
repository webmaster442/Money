using System.Diagnostics.CodeAnalysis;
using System.IO;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data;
using Money.Data.Dto;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class ImportExcelCommand : Command<ImportSetting>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportExcelCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ImportSetting settings)
        {
            try
            {
                using (var srtream = File.OpenRead(settings.FileName))
                {
                    var data = MiniExcel.Query<ExportRow>(srtream).ToList();
                    var (createdCategory, createdEntry) = _writeOnlyData.Import(data);
                    Ui.Success($"Imported {createdCategory} categories and {createdEntry} entries");
                    return Constants.Success;
                }
            }
            catch (Exception ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
