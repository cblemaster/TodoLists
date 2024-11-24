
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class ImportantList : TodoList
{
    public ImportantList(uint todoListId, string name, Todo[] todos) : base(todoListId, name, todos)
    {
        if (todos.Any(t => !t.IsImportant))
        {
            throw new ArgumentException("Important list cannot contain todos that are not important.", nameof(todos));
        }
    }
}
