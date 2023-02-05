using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;

using Money.Data.Dto;

namespace Money.Commands
{
    internal sealed class ImportBackupCommand : AsyncCommand<ImportSetting>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportBackupCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }


        public override async Task<int> ExecuteAsync(CommandContext context,
                                                     ImportSetting settings)
        {
            List<DataRow> buffer = new(_writeOnlyData.ChunkSize);
            int sumCategory = 0;
            int sumEntry = 0;

            if (!Ui.Confirm(Resources.WarnWillReplaceDbContents))
            {
                return Constants.Aborted;
            }

            await _writeOnlyData.ClearDb();

            try
            {
                using (FileStream srtream = File.OpenRead(settings.FileName))
                {
                    using GZipStream compressed = new GZipStream(srtream, CompressionMode.Decompress, true);
                    using var reader = new StreamReader(compressed, Encoding.UTF8);
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;

                        if (buffer.Count > (_writeOnlyData.ChunkSize - 1))
                        {
                            (int createdCategory, int createdEntry) = await _writeOnlyData.ImportAsync(buffer);
                            buffer.Clear();
                            sumCategory += createdCategory;
                            sumEntry += createdEntry;
                        }

                        buffer.Add(DataRow.Parse(line));
                    }
                }

                if (buffer.Count > 0)
                {
                    (int createdCategory, int createdEntry) = await _writeOnlyData.ImportAsync(buffer);
                    buffer.Clear();
                    sumCategory += createdCategory;
                    sumEntry += createdEntry;
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
