
namespace TodoLists.Domain;

public class TodoList : Entity
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Todo> Todos { get; set; } = [];
}
