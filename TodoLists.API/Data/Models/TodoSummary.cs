
namespace TodoLists.API.Data.Models;

internal sealed class TodoSummary
{
    public required Guid Id { get; init; }
    public required string Description { get; init; }
    public DateOnly? DueDate { get; init; }
    public bool IsImportant { get; init; }
    public bool IsComplete { get; init; }
    public required Guid TodoListId { get; init; }
    public required string ListName { get; init; }
}
