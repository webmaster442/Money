namespace Money.Infrastructure;

internal interface ISettingsManager
{
    string FileName { get; }

    Settings Load();
    void Save(Settings settings);
}