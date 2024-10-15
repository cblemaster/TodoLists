namespace TodoLists.Server.Entities;

public partial class TodoList
{
    public int TodoListId { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Todo> Todos { get; set; } = [];
}
