using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace Money.Commands
{
    internal sealed class ExportBackupCommand : AsyncCommand<ExportSetting>
    {
        private readonly IReadonlyData _readonlyData;

        public ExportBackupCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context,
                                               [NotNull] ExportSetting settings)
        {
            try
            {
                IList<Data.Dto.ExportRow> data = await _readonlyData.ExportAsync(settings.StartDate, settings.EndDate);

                using (FileStream stream = File.Create(settings.FileName))
                {
                    using (GZipStream compressed = new GZipStream(stream, CompressionLevel.SmallestSize, true))
                    {
                        compressed.WriteJson(data);
                    }
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
