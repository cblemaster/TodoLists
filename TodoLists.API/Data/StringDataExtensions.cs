
namespace TodoLists.API.Data;

internal static class StringDataExtensions
{
    internal static string FormattedValidationErrors(this IEnumerable<string> errors) => $"{Constants.Constants.VALIDATION_ERROR_ROOT}{string.Join('\n', errors)}";
    internal static string FormattedError(this string error) => $"{Constants.Constants.ERROR_ROOT}{error}";
    internal static string NotFound<T>() => $"{typeof(T)} not found.";
}
