﻿using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal class ExportExcelCommand : Command<ExportSetting>
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
                var data = _readonlyData.Export(settings.StartDate, settings.EndDate);

                using (var srtream = File.Create(settings.FileName))
                {
                    srtream.SaveAs(data);
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