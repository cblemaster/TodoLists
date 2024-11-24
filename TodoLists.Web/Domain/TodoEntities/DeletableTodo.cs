
using TodoLists.Web.Domain.TodoListEntities;

namespace TodoLists.Web.Domain.TodoEntities;

public class DeletableTodo : Todo
{
    public DeletableTodo(int todoId, int todoListId, string title, DateOnly? dueDate, bool isImportant, bool isComplete, TodoList todoList) : base(todoId, todoListId, title, dueDate, isImportant, isComplete, todoList)
    {
        if (isImportant)
        {
            throw new ArgumentException("Important todos cannot be deleted.", nameof(isImportant));
        }
    }
}
