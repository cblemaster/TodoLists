namespace TodoLists.Server.Entities;

public partial class TodoList
{
    public int TodoListId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Todo> Todos { get; set; } = [];
}
