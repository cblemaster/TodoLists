
namespace TodoLists.Web.Entities;

public class TodoList
{
    public uint TodoListId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public virtual ICollection<Todo> Todos { get; private set; } = [];

        //if (todoListId <= 0) { throw new ArgumentException("List id must be greater than zero (0).", nameof(todoListId)); }
        //if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("List name is required and cannot consist of only whitespace characters.", nameof(name)); }
        //if (name.Length > 255) { throw new ArgumentException("List name must be 255 or fewer characters.", nameof(name)); }
}
