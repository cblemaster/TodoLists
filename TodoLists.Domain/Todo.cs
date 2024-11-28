
namespace TodoLists.Domain;

public class Todo : Entity
{
    public string Description { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }
    public Guid TodoListId { get; set; }
    public virtual TodoList TodoList { get; set; } = new();

    public static Todo NotFound() => new() { Id = Guid.Empty, Description = "Todo not found." };
    public static Todo NotValid(IEnumerable<string> validationErrors) => new() { Id = Guid.Empty, Description = "The following validation error(s) occurred:\n" + string.Join('\n', validationErrors) };
    public static Todo Error(string error) => new() { Id = Guid.Empty, Description = "The following error occurred:\n" + error };
    public static Todo NoContent() => new() { Id = Guid.Empty, Description = "No content." };
}
