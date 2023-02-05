using System.Data;
using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.Data.Dto;

namespace Money.Commands
{
    internal sealed class ImportExcelCommand : AsyncCommand<ImportSetting>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportExcelCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context,
                                                     [NotNull] ImportSetting settings)
        {
            try
            {
                int sumCategory = 0;
                int sumEntry = 0;
                using (FileStream stream = File.OpenRead(settings.FileName))
                {
                    var chunks = MiniExcel.Query<Data.Dto.DataRow>(stream).Chunk(_writeOnlyData.ChunkSize);
                    foreach (var chunk in chunks)
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
}
