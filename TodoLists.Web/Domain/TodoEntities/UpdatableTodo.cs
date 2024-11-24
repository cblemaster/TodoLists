
using TodoLists.Web.Domain.TodoListEntities;

namespace TodoLists.Web.Domain.TodoEntities;

public class UpdatableTodo : Todo
{
    public UpdatableTodo(int todoId, int todoListId, string title, DateOnly? dueDate, bool isImportant, bool isComplete, TodoList todoList) : base(todoId, todoListId, title, dueDate, isImportant, isComplete, todoList)
    {
        if (isComplete)
        {
            throw new ArgumentException("Completed todos cannot be updated.", nameof(isComplete));
        }
    }
}
