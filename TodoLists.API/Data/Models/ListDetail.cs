
namespace TodoLists.API.Data.Models;

internal sealed class ListDetail
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<TodoSummary> Todos { get; init; }
}
