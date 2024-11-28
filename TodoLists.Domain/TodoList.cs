
namespace TodoLists.Domain;

public class TodoList : Entity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Todo> Todos { get; set; } = [];

    public static TodoList NotFound() => new() { Id = Guid.Empty, Name = "Todo list not found." };
    public static TodoList NotValid(IEnumerable<string> validationErrors) => new() { Id = Guid.Empty, Name = "The following validation error(s) occurred:\n" + string.Join('\n', validationErrors) };
    public static TodoList Error(string error) => new() { Id = Guid.Empty, Name = "The following error occurred:\n" + error };
    public static TodoList NoContent() => new() { Id = Guid.Empty, Name = "No content." };
}
