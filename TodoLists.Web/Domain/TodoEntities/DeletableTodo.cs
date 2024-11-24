
namespace TodoLists.Web.Domain.TodoEntities;

public class DeletableTodo : Todo
{
    public DeletableTodo(uint todoId, string title, DateOnly? dueDate, bool isImportant, bool isComplete) : base(todoId, title, dueDate, isImportant, isComplete)
    {
        if (isImportant)
        {
            throw new ArgumentException("Important todos cannot be deleted.", nameof(isImportant));
        }
    }
}
