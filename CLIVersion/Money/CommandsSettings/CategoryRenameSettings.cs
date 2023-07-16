using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings;

internal class CategoryRenameSettings : CommandSettings
{
    [Description("Old category name")]
    [CommandArgument(0, "[oldcategory]")]
    public string OldCategoryName { get; set; }

    [Description("New category name")]
    [CommandArgument(1, "[newcategory]")]
    public string NewCategoryName { get; set; }

    public CategoryRenameSettings()
    {
        OldCategoryName = string.Empty;
        NewCategoryName = string.Empty;
    }

    public override ValidationResult Validate()
    {
        ValidationResultBuilder builder = new();

        return
            builder
            .AddIfNullOrWhiteSpace(OldCategoryName, Resources.ErrorCategoryNameNull)
            .AddIfNullOrWhiteSpace(NewCategoryName, Resources.ErrorCategoryNameNull)
            .Build();
    }
}
