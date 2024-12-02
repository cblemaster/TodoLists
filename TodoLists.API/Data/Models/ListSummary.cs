
namespace TodoLists.API.Data.Models;

internal sealed class ListSummary
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int CountOfTodosNotComplete { get; init; }
}
