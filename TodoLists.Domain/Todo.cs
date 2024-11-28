
namespace TodoLists.Domain;

public class Todo : Entity
{
    public string Description { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }
    public Guid TodoListId { get; set; }
    public virtual TodoList TodoList { get; set; } = new();
}
