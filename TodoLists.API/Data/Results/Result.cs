
namespace TodoLists.API.Data.Results;

internal sealed class Result<T>
{
    internal required ResultType ResultType { get; init; }
    internal required string Message { get; init; }
    internal T Payload { get; init; } = default(T)!;
}
