using System.ComponentModel;

using Spectre.Console;

namespace Money.CommandsSettings
{
    internal class CategoryRenameSettings : CommandSettings
    {
        [Description("Old category name")]
        [CommandArgument(0, "[oldcategory]")]
        public string OldCategoryName { get; set; }

        [Description("New category name")]
        [CommandArgument(1, "[oldcategory]")]
        public string NewCategoryName { get; set; }

        public CategoryRenameSettings()
        {
            OldCategoryName = string.Empty;
            NewCategoryName = string.Empty;
        }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrEmpty(OldCategoryName)
                || string.IsNullOrEmpty(NewCategoryName))
            {
                return ValidationResult.Error(Resources.ErrorCategoryNameNull);
            }
            return ValidationResult.Success();
        }
    }
}
