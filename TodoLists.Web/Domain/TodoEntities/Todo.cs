
using TodoLists.Web.Domain.TodoListEntities;

namespace TodoLists.Web.Domain.TodoEntities;

public class Todo
{
    public int TodoId { get; set; }
    public int TodoListId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public bool IsImportant { get; set; }
    public bool IsComplete { get; set; }
    public virtual TodoList TodoList { get; set; } = default!;

    public Todo(int todoId, int todoListId, string title, DateOnly? dueDate, bool isImportant, bool isComplete, TodoList todoList)
    {
        if (todoId == 0) { throw new ArgumentException("Todo id must be non-zero.", nameof(todoId)); }
        if (todoListId == 0) { throw new ArgumentException("Todo list id must be non-zero.", nameof(todoListId)); }
        if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentException("Todo title is required and cannot consist of only whitespace characters.", nameof(title)); }
        if (title.Length > 255) { throw new ArgumentException("Todo title must be 255 or fewer characters.", nameof(title)); }

        TodoId = todoId;
        TodoListId = todoListId;
        Title = title;
        DueDate = dueDate;
        IsImportant = isImportant;
        IsComplete = isComplete;
        TodoList = todoList;
    }
}
