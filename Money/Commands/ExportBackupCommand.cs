using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data;
using Money.Extensions;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class ExportBackupCommand : Command<ExportSetting>
    {
        private readonly IReadonlyData _readonlyData;

        public ExportBackupCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ExportSetting settings)
        {
            try
            {
                var data = _readonlyData.Export(settings.StartDate, settings.EndDate);

                using (var stream = File.Create(settings.FileName))
                {
                    using (var compressed = new GZipStream(stream, CompressionLevel.SmallestSize, true))
                    {
                        compressed.WriteJson(data);
                    }
                }
                Ui.Success($"Successfully written {data.Count} entries to {settings.FileName}");
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
