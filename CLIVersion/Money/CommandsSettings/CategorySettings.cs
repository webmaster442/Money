using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings;

internal sealed class CategorySettings : CommandSettings
{
    [Description("Category name")]
    [CommandArgument(0, "[category]")]
    public string CategoryName { get; set; }

    public CategorySettings()
    {
        CategoryName = string.Empty;
    }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrEmpty(CategoryName))
            return ValidationResult.Error(Resources.ErrorCategoryNameNull);

        return ValidationResult.Success();
    }
}
