
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class DeletableTodoList : TodoList
{
    public DeletableTodoList(uint todoListId, string name, Todo[] todos) : base(todoListId, name, todos)
    {
        if (todos.Any(t => !t.IsComplete))
        {
            throw new ArgumentException("List cannot be deleted because it has one or more todos that are not complete.", nameof(todos));
        }
    }
}
