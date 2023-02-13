using MiniExcelLibs;

namespace Money.Commands;

internal sealed class ImportExcelCommand : AsyncCommand<ImportSetting>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public ImportExcelCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context,
                                                 ImportSetting settings)
    {
        try
        {
            int sumCategory = 0;
            int sumEntry = 0;
            using (FileStream stream = File.OpenRead(settings.FileName))
            {
                IEnumerable<Data.Dto.DataRowExcel[]> chunks = MiniExcel.Query<Data.Dto.DataRowExcel>(stream).Chunk(_writeOnlyData.ChunkSize);
                foreach (Data.Dto.DataRowExcel[] chunk in chunks)
                {
                    (int createdCategory, int createdEntry) = await _writeOnlyData.ImportAsync(chunk);
                    sumCategory += createdCategory;
                    sumEntry += createdEntry;
                }
            }

            Ui.Success(Resources.SuccesImport, sumCategory, sumEntry);
            return Constants.Success;

        }
        catch (Exception ex)
        {
            Ui.PrintException(ex);
            return Constants.IoError;
        }
    }
}
