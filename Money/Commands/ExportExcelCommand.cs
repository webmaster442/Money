using MiniExcelLibs;

using Money.Data.Dto;

namespace Money.Commands;

internal sealed class ExportExcelCommand : AsyncCommand<ExportSetting>
{
    private readonly IReadonlyData _readonlyData;

    public ExportExcelCommand(IReadonlyData readonlyData)
    {
        _readonlyData = readonlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context,
                                                 ExportSetting settings)
    {
        settings.AppendXlsxToFileNameWhenNeeded();

        try
        {
            IList<DataRowExcel> data = await _readonlyData.ExportAsync(settings.StartDate, settings.EndDate);

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
