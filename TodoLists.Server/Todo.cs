namespace TodoLists.Server;

public partial class Todo
{
    public int TodoId { get; set; }

    public int TodoListId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateOnly? DueDate { get; set; }

    public bool IsImportant { get; set; }

    public bool IsComplete { get; set; }

    public virtual TodoList TodoList { get; set; } = null!;
}
