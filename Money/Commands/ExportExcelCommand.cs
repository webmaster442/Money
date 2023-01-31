using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data;
using Money.Properties;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class ExportExcelCommand : Command<ExportSetting>
    {
        private readonly IReadonlyData _readonlyData;

        public ExportExcelCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ExportSetting settings)
        {
            try
            {
                IList<Data.Dto.ExportRow> data = _readonlyData.Export(settings.StartDate, settings.EndDate);

                using (FileStream srtream = File.Create(settings.FileName))
                {
                    srtream.SaveAs(data);
                }
                Ui.Success(Resources.SuccessExport, data.Count, settings.FileName);
                return Constants.Success;
            }
            catch (Exception ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
