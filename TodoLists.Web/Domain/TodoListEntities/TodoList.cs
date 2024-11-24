
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class TodoList : List<Todo>
{
    public uint TodoListId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public TodoList(uint todoListId, string name, Todo[] todos) : base(todos)
    {
        if (todoListId == 0) { throw new ArgumentException("Todo list id must be non-zero.", nameof(todoListId)); }
        if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("List name is required and cannot consist of only whitespace characters.", nameof(name)); }
        if (name.Length > 255) { throw new ArgumentException("List name must be 255 or fewer characters.", nameof(name)); }

        TodoListId = todoListId;
        Name = name;
    }
}
