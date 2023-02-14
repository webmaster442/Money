using Money.Data.Dto;
using Money.Infrastructure;

namespace Money.Commands;

internal sealed class SyncPushCommand : SyncCommandBase
{
    private readonly IReadonlyData _readonlyData;
    private readonly Settings _settings;

    public SyncPushCommand(IReadonlyData readonlyData, Settings settings)
    {
        _readonlyData = readonlyData;
        _settings = settings;
    }

    private async Task WriteToFile(string currentFileName, List<string> rowBuffer)
    {
        string diskFile = Path.Combine(_settings.GitRepoPathUsedForSyncOnDisk, currentFileName);
        await File.AppendAllLinesAsync(diskFile, rowBuffer);
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        DateTime lastOnDisk = await GetLastInsertDateFile(_settings.GitRepoPathUsedForSyncOnDisk);
        DateTime lastOnDB = await _readonlyData.GetLastInsertDate();

        if (lastOnDB <= lastOnDisk)
        {
            return Constants.Success;
        }

        string lastFileName = $"{lastOnDisk.Year}-{lastOnDisk.Month}-{lastOnDisk.Day}.csv";
        string currentFileName = $"{lastOnDisk.Year}-{lastOnDisk.Month}-{lastOnDisk.Day}.csv";

        List<string> rowBuffer = new(100);

        await foreach (var data in _readonlyData.ExportBackupAsync(lastOnDisk))
        {
            currentFileName = $"{lastOnDisk.Year}-{lastOnDisk.Month}-{lastOnDisk.Day}.csv";
            
            if (currentFileName != lastFileName 
                || rowBuffer.Count > 99)
            {
                await WriteToFile(currentFileName, rowBuffer);
                rowBuffer.Clear();
                lastFileName = currentFileName;
            }

            string line = DtoAdapter.ToCsvLine(data);
            rowBuffer.Add(line);
        }

        if (rowBuffer.Count > 0)
        {
            await WriteToFile(currentFileName, rowBuffer);
            rowBuffer.Clear();
        }

        return await Push(lastOnDB, _settings.GitRepoPathUsedForSyncOnDisk);
    }
}
