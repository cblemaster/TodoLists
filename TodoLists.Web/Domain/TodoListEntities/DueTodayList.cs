
using TodoLists.Web.Domain.TodoEntities;

namespace TodoLists.Web.Domain.TodoListEntities;

public class DueTodayList : TodoList
{
    public DueTodayList(uint todoListId, string name, Todo[] todos) : base(todoListId, name, todos)
    {
        if (todos.Any(t => t.DueDate.HasValue && t.DueDate != DateOnly.FromDateTime(DateTime.Today)))
        {
            throw new ArgumentException("Due today list cannot contain todos that are not due today.", nameof(todos));
        }
    }
}
