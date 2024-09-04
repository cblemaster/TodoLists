namespace TodoLists.Server.Entities;

public partial class Todo
{
    public int TodoId { get; set; }

    public int TodoListId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? DueDate { get; set; }  //TODO >> using datetime when I would really prefer dateonly; however nullable dateonly does not serialize to/deserialize from json, workarounds (converters) exist but I couldn't get any workarounds to...work

    public bool IsImportant { get; set; }

    public bool IsComplete { get; set; }

    public virtual TodoList TodoList { get; set; } = null!;
}
