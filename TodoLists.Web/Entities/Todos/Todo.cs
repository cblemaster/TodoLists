
using TodoLists.Web.Entities.Lists;

namespace TodoLists.Web.Entities.Todos;

public class Todo
{
    public required int TodoId { get; set; }
    public int ListId { get; set; }
    public required string Description { get; set; }
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }
    public virtual ListDetail List { get; set; } = default!;

    public Todo()
    {
        //if (todoId == 0) { throw new ArgumentException("Todo id must be non-zero.", nameof(todoId)); }
        //if (todoListId == 0) { throw new ArgumentException("Todo list id must be non-zero.", nameof(todoListId)); }
        //if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentException("Todo title is required and cannot consist of only whitespace characters.", nameof(title)); }
        //if (title.Length > 255) { throw new ArgumentException("Todo title must be 255 or fewer characters.", nameof(title)); }
    }
}
