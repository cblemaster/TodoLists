
namespace TodoLists.API.Data.Models;

internal sealed class ListDetail
{
    internal required Guid Id { get; init; }
    internal required string Name { get; init; }
    internal required IEnumerable<TodoSummary> Todos { get; init; }
}
