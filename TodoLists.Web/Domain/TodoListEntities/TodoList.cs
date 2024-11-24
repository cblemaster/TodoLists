
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class TodoList
{
    public int TodoListId { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Todo> Todos { get; set; }

    public TodoList(int todoListId, string name, Todo[] todos)
    {
        if (todoListId == 0) { throw new ArgumentException("Todo list id must be non-zero.", nameof(todoListId)); }
        if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("List name is required and cannot consist of only whitespace characters.", nameof(name)); }
        if (name.Length > 255) { throw new ArgumentException("List name must be 255 or fewer characters.", nameof(name)); }

        TodoListId = todoListId;
        Name = name;
        Todos = todos;
    }
}
