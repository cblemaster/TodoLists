
namespace TodoLists.API.Validation;

public sealed class ValidationResult
{
    public bool IsValid { get; init; }
    public IEnumerable<string> Errors { get; init; } = [];
}
