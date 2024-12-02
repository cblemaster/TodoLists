
namespace TodoLists.API.Validation;

internal sealed class ValidationResult
{
    internal bool IsValid { get; init; }
    internal IEnumerable<string> Errors { get; init; } = [];
}
