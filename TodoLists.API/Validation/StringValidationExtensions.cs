
namespace TodoLists.API.Validation;

internal static class StringValidationExtensions
{
    // TODO >> this is the same logic as ValidateTodoDescription(), only the error messages differ
    internal static ValidationResult ValidateTodoListName(this string s)
    {
        bool isValid = true;
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(s))
        {
            isValid = false;
            errors.Add("List name is required and cannot be only whitespace characters.");
        }
        else if (s.Length > 255)
        {
            isValid = false;
            errors.Add("List name cannot exceed 255 characters.");
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
            errors.Add("Todo description is required and cannot be only whitespace characters.");
        }
        else if (s.Length > 255)
        {
            isValid = false;
            errors.Add("Todo description cannot exceed 255 characters.");
        }
        return new ValidationResult() { IsValid = isValid, Errors = errors };
    }
}
