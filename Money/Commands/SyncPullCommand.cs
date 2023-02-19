using System;

using Money.Data.Dto;
using Money.Infrastructure;

namespace Money.Commands;
internal sealed class SyncPullCommand : SyncCommandBase
{
    private readonly IReadonlyData _readonlyData;
    private readonly IWriteOnlyData _writeOnlyData;
    private readonly Settings _settings;

    public SyncPullCommand(IReadonlyData readonlyData, IWriteOnlyData writeOnlyData, ISettingsManager settingsManager)
    {
        _readonlyData = readonlyData;
        _writeOnlyData = writeOnlyData;
        _settings = settingsManager.Load();
    }
    private static DateOnly DateFromFileName(string fn)
    {
        var name = Path.GetFileNameWithoutExtension(fn);
        return DateOnly.Parse(name.Replace('-', '.'));
    }

    private static IEnumerable<DataRowBackup> ReadFromDisk(string folder, DateTime startTime)
    {
        var files = Directory
                        .GetFiles(folder, "*.csv")
                        .Where(fn => DateFromFileName(fn) >= startTime.ToDateOnly());

        foreach (var file in files)
        {
            using (var reader = File.OpenText(file))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var data = DtoAdapter.FromCsvLine(line);
                        if (data.AddedOn >= startTime)
                            yield return data;
                    }
                }
            }
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var result = _settings.Validate();
        if (!result.Successful)
        {
            return Ui.Error(result.Message ?? "");
        }

        await Pull(_settings.GitRepoPathUsedForSyncOnDisk);

        DateTime lastOnDB = await _readonlyData.GetLastInsertDate();

        if (!TryGetLastInsertDateFile(_settings.GitRepoPathUsedForSyncOnDisk, out DateTime lastOnDisk))
        {
            lastOnDisk = await _readonlyData.GetFirstInsertDate();
        }


        if (lastOnDB >= lastOnDisk)
        {
            return Constants.Success;
        }

        IEnumerable<DataRowBackup> onDiskData = ReadFromDisk(_settings.GitRepoPathUsedForSyncOnDisk, lastOnDisk);

        var (createdCategory, createdEntry) = await _writeOnlyData.ImportBackupAsync(onDiskData);

        Ui.Success(Resources.SuccesImport, createdCategory, createdEntry);

        return Constants.Success;
    }
}
