
namespace TodoLists.Web.Domain.TodoEntities;

public class Todo
{
    public uint TodoId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateOnly? DueDate { get; private set; }
    public bool IsImportant { get; private set; }
    public bool IsComplete { get; private set; }

    public Todo(uint todoId, string title, DateOnly? dueDate, bool isImportant, bool isComplete)
    {
        if (todoId == 0) { throw new ArgumentException("Todo id must be non-zero.", nameof(todoId)); }
        if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentException("Todo title is required and cannot consist of only whitespace characters.", nameof(title)); }
        if (title.Length > 255) { throw new ArgumentException("Todo title must be 255 or fewer characters.", nameof(title)); }

        TodoId = todoId;
        Title = title;
        DueDate = dueDate;
        IsImportant = isImportant;
        IsComplete = isComplete;
    }
}
