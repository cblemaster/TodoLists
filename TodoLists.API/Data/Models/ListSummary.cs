
namespace TodoLists.API.Data.Models;

internal sealed class ListSummary
{
    internal required Guid Id { get; init; }
    internal required string Name { get; init; } = string.Empty;
    internal required int CountOfTodosNotComplete { get; init; }
}
