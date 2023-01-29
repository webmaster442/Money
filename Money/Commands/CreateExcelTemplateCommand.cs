using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data.Dto;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class CreateExcelTemplateCommand : Command<CreateExcelTemplateSettings>
    {
        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] CreateExcelTemplateSettings settings)
        {
            try
            {
                var data = new List<ExportRow>();

                using (var srtream = File.Create(settings.FileName))
                {
                    srtream.SaveAs(data);
                }
                Ui.Success($"Successfully created import template {settings.FileName}");
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
