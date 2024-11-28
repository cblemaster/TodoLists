
using TodoLists.Domain;

namespace TodoLists.API.Data.Models;

internal sealed class TodoSummary
{
    internal Guid Id { get; init; }
    internal string Description { get; init; } = string.Empty;
    internal DateOnly? DueDate { get; init; }
    internal bool IsImportant { get; init; }
    internal bool IsComplete { get; init; }
    internal Guid TodoListId { get; init; }
    internal string ListName { get; init; } = string.Empty;
}
