
namespace TodoLists.API.Validation;

internal static class StringValidationExtensions
{
    // TODO: Validation needs an overhaul...duplicated code here, only the errors differ
    internal static ValidationResult ValidateTodoListName(this string s)
    {
        bool isValid = true;
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(s))
        {
            isValid = false;
            errors.Add(Constants.Constants.LIST_NAME_REQUIRED);
        }
        else if (s.Length > Constants.Constants.LIST_NAME_MAX_LENGTH)
        {
            isValid = false;
            errors.Add(Constants.Constants.LIST_NAME_LENGTH);
        }
        return new ValidationResult() { IsValid = isValid, Errors = errors };
    }

    internal static ValidationResult ValidateTodoDescription(this string s)
    {
        bool isValid = true;
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(s))
        {
            isValid = false;
            errors.Add(Constants.Constants.TODO_DESC_REQUIRED);
        }
        else if (s.Length > 255)
        {
            isValid = false;
            errors.Add(Constants.Constants.TODO_DESC_LENGTH);
        }
        return new ValidationResult() { IsValid = isValid, Errors = errors };
    }
}
