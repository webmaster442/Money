using MiniExcelLibs;

using Money.Data.Dto;

namespace Money.Commands;

internal sealed class CreateExcelTemplateCommand : AsyncCommand<CreateExcelTemplateSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context,
                                                 CreateExcelTemplateSettings settings)
    {
        settings.AppendXlsxToFileNameWhenNeeded();

        try
        {
            List<DataRow> data = new List<DataRow>();

            using (FileStream srtream = File.Create(settings.FileName))
            {
                await srtream.SaveAsAsync(data);
            }
            Ui.Success(Resources.SuccessCreatedImportTemplate, settings.FileName);
            return Constants.Success;
        }
        catch (Exception ex)
        {
            Ui.PrintException(ex);
            return Constants.IoError;
        }
    }
}
