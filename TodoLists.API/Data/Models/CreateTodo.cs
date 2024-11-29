
namespace TodoLists.API.Data.Models;

internal sealed class CreateTodo
{
    public required Guid Id { get; set; }
    public required string Description { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }
    public required Guid TodoListId { get; set; }
}
