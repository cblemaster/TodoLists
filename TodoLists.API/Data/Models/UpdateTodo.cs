
namespace TodoLists.API.Data.Models;

internal sealed class UpdateTodo
{
    public required Guid Id { get; set; }
    public required string Description { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; init; }
    public bool IsComplete { get; set; }
    public Guid TodoListId { get; set; }
}
