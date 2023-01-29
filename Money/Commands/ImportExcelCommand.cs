using System.Data;
using System.Diagnostics.CodeAnalysis;

using MiniExcelLibs;

using Money.CommandsSettings;
using Money.Data;
using Money.Data.Dto;
using Money.Extensions;

using Spectre.Console.Cli;

namespace Money.Commands
{
    internal sealed class ImportExcelCommand : Command<ImportSetting>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportExcelCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ImportSetting settings)
        {
            try
            {
                using (FileStream srtream = File.OpenRead(settings.FileName))
                {
                   var dataTable = MiniExcel.QueryAsDataTable(srtream);

                    List<ExportRow> data = new(dataTable.Rows.Count);

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ExportRow row = new ExportRow
                        {
                            Date = dataRow[0].ToDateOnly(),
                            Description = dataRow[1]?.ToString() ?? throw new InvalidProgramException("Description can't be empty"),
                            Ammount = dataRow[2].ToDouble(),
                            AddedOn = dataRow[3].ToDateTime(DateTime.Now),
                            CategoryName = dataRow[4]?.ToString() ?? throw new InvalidProgramException("Category name can't be empty"),
                        };
                        data.Add(row);
                    }

                    (int createdCategory, int createdEntry) = _writeOnlyData.Import(data);
                    Ui.Success($"Imported {createdCategory} categories and {createdEntry} entries");
                    return Constants.Success;
                }
            }
            catch (Exception ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
