
using Microsoft.EntityFrameworkCore;
using TodoLists.API.Data.Models;
using TodoLists.API.Data.Results;
using TodoLists.API.Validation;
using TodoLists.Domain;
using Strings = TodoLists.API.Data.Constants.Constants;

namespace TodoLists.API.Data;

internal partial class TodoListDbContext
{
    #region queries
    internal IEnumerable<ListSummary> GetListSummaries() =>
        TodoLists
            .Include(t => t.Todos)
            .Select(t => new ListSummary()
            {
                Id = t.Id,
                Name = t.Name,
                CountOfTodosNotComplete = t.Todos.Count(t => !t.IsComplete)
            }
            )
            .AsEnumerable();
    internal async Task<TodoList?> GetListDetailAsync(Guid id) =>
        await TodoLists.Include(t => t.Todos).SingleOrDefaultAsync(t => t.Id == id);
    internal async Task<TodoSummary?> GetTodoSummaryAsync(Guid id) =>
        await Todos
            .Include(t => t.TodoList)
            .Select(t => new TodoSummary()
            {
                Id = t.Id,
                Description = t.Description,
                DueDate = t.DueDate,
                IsImportant = t.IsImportant,
                IsComplete = t.IsComplete,
                TodoListId = t.TodoList.Id,
                ListName = t.TodoList.Name
            }
            )
            .SingleOrDefaultAsync(t => t.Id == id);
    internal async Task<Todo?> GetTodoAsync(Guid id) =>
        await Todos.Include(t => t.TodoList).SingleOrDefaultAsync(t => t.Id == id);
    #endregion queries

    #region commands
    internal async Task<Result<TodoList>> CreateTodoListAsync(CreateTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if (!vr.IsValid)
        {
            return new Result<TodoList>() { Message = Strings.FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            await TodoLists.AddAsync(new TodoList() { Id = dto.Id, Name = dto.Name });
            await SaveChangesAsync();
            return new Result<TodoList>() { Message = Strings.CREATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> CreateTodoAsync(CreateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if (!vr.IsValid)
        {
            return new Result<Todo>() { Message = Strings.FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            await Todos.AddAsync(new Todo() { Id = dto.Id, Description = dto.Description });
            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.CREATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<TodoList>> DeleteTodoListAsync(Guid id)
    {
        if ((await GetListDetailAsync(id) is not TodoList list))
        {
            return new Result<TodoList>() { Message = Strings.NotFound<TodoList>(), ResultType = ResultType.NotFound };
        }
        else if (list.Todos.Any(t => !t.IsComplete))
        {
            return new Result<TodoList>() { Message = Strings.CANNOT_DELETE_TODO_LISTS_WITH_TODOS_NOT_COMPLETE, ResultType = ResultType.Error };
        }
        else
        {
            TodoLists.Remove(list);
            await SaveChangesAsync();
            return new Result<TodoList>() { Message = Strings.DELETED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> DeleteTodoAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = Strings.NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else if (todo.IsImportant)
        {
            return new Result<Todo>() { Message = Strings.CANNOT_DELETE_IMPORTANT_TODOS, ResultType = ResultType.Error };
        }
        else
        {
            Todos.Remove(todo);
            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.DELETED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<TodoList>> RenameTodoListAsync(Guid id, RenameTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if ((await GetListDetailAsync(id)) is not TodoList list)
        {
            return new Result<TodoList>() { Message = Strings.NotFound<TodoList>(), ResultType = ResultType.NotFound };
        }
        else if (!vr.IsValid)
        {
            return new Result<TodoList>() { Message = Strings.FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else if (list.Name != dto.Name)
        {
            list.Name = dto.Name;
            await SaveChangesAsync();
        }
        return new Result<TodoList>() { Message = Strings.RENAMED_SUCCESSFULLY, ResultType = ResultType.Success };
    }
    internal async Task<Result<Todo>> UpdateTodoAsync(Guid id, UpdateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = Strings.NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else if (!vr.IsValid)
        {
            return new Result<Todo>() { Message = Strings.FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            if (todo.Description != dto.Description) { todo.Description = dto.Description; }
            if (todo.DueDate != dto.DueDate) { todo.DueDate = dto.DueDate; }
            if (todo.IsImportant != dto.IsImportant) { todo.IsImportant = dto.IsImportant; }
            if (todo.IsComplete != dto.IsComplete) { todo.IsComplete = dto.IsComplete; }
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }

            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.UPDATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> ToggleTodoImportanceAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = Strings.NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            todo.IsImportant = !todo.IsImportant;
            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.TOGGLED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> ToggleTodoCompletionAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = Strings.NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            todo.IsComplete = !todo.IsComplete;
            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.TOGGLED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> MoveTodoToListAsync(Guid id, UpdateTodo dto)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = Strings.NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }
            await SaveChangesAsync();
            return new Result<Todo>() { Message = Strings.MOVED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    #endregion commands

}
