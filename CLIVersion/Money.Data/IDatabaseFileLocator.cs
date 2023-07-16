namespace Money.Data;

public interface IDatabaseFileLocator
{
    bool DatabaseFileExists
        => File.Exists(DatabasePath);

    string DatabasePath { get; }
}