
namespace TodoLists.API.Data.Models;

internal sealed class ListSummary
{
    internal Guid Id { get; init; }
    internal string Name { get; init; } = string.Empty;
    internal int CountOfTodosNotComplete { get; init; }
}
