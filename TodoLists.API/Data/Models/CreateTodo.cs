
namespace TodoLists.API.Data.Models;

internal sealed class CreateTodo
{
    internal required Guid Id { get; set; }
    internal string Description { get; set; } = string.Empty;
    internal DateOnly? DueDate { get; set; }
    internal bool IsImportant { get; set; }
    internal bool IsComplete { get; set; }
    internal Guid TodoListId { get; set; }
}
