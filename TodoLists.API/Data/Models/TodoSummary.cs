
using TodoLists.Domain;

namespace TodoLists.API.Data.Models;

internal sealed class TodoSummary
{
    internal required Guid Id { get; init; }
    internal required string Description { get; init; }
    internal DateOnly? DueDate { get; init; }
    internal bool IsImportant { get; init; }
    internal bool IsComplete { get; init; }
    internal required Guid TodoListId { get; init; }
    internal required string ListName { get; init; }
}
