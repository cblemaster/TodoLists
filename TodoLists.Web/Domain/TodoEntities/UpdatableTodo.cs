
namespace TodoLists.Web.Domain.TodoEntities;

public class UpdatableTodo : Todo
{
    public UpdatableTodo(uint todoId, string title, DateOnly? dueDate, bool isImportant, bool isComplete) : base(todoId, title, dueDate, isImportant, isComplete)
    {
        if (isComplete)
        {
            throw new ArgumentException("Completed todos cannot be updated.", nameof(isComplete));
        }
    }
}
