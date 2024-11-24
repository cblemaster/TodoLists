
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class CompletedList : TodoList
{
    public CompletedList(int todoListId, string name, Todo[] todos) : base(todoListId, name, todos)
    {
        if (todos.Any(t => !t.IsComplete))
        {
            throw new ArgumentException("Completed list cannot contain todos that are not complete.", nameof(todos));
        }
    }
}
