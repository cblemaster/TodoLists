
using Microsoft.EntityFrameworkCore;
using TodoLists.API.Data.Models;
using TodoLists.API.Data.Results;
using TodoLists.API.Validation;

using TodoLists.Domain;

namespace TodoLists.API.Data;

internal class TodoListDbContext : DbContext
{
    #region strings - extract into separate class
    private const string CREATED_SUCCESSFULLY = "Created successfully.";
    private const string RENAMED_SUCCESSFULLY = "Renameded successfully.";
    private const string DELETED_SUCCESSFULLY = "Deleted successfully.";
    private const string TOGGLED_SUCCESSFULLY = "Toggled successfully.";
    private const string UPDATED_SUCCESSFULLY = "Updated successfully.";
    private const string MOVED_SUCCESSFULLY = "Moved successfully.";
    private const string CANNOT_DELETE_IMPORTANT_TODOS = "Cannot delete important todos.";
    private const string CANNOT_DELETE_TODO_LISTS_WITH_TODOS_NOT_COMPLETE = "Cannot delete todo list because it contains todos that are not complete.";

    private string FormattedValidationErrors(IEnumerable<string> errors) => $"The following validation errors occurred:\n{string.Join('\n', errors)}";
    private string FormattedError(string error) => $"The following error occurred:\n{error}";
    private string NotFound<T>() => $"{typeof(T)} not found.";
    #endregion strings - extract into separate class

    public DbSet<Todo> Todos { get; set; } = default!;
    public DbSet<TodoList> TodoLists { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.TodoList).WithMany(p => p.Todos)
                .HasForeignKey(d => d.TodoListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Todo_TodoList");
        });
        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.ToTable("TodoList");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
    }

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
    internal async Task<Result<TodoList>> CreateTodoListAsync(CreateTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if (!vr.IsValid)
        {
            return new Result<TodoList>() { Message = FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            await TodoLists.AddAsync(new TodoList() { Id = dto.Id, Name = dto.Name });
            await SaveChangesAsync();
            return new Result<TodoList>() { Message = CREATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> CreateTodoAsync(CreateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if (!vr.IsValid)
        {
            return new Result<Todo>() { Message = FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            await Todos.AddAsync(new Todo() { Id = dto.Id, Description = dto.Description });
            await SaveChangesAsync();
            return new Result<Todo>() { Message = CREATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<TodoList>> DeleteTodoListAsync(Guid id)
    {
        if ((await GetListDetailAsync(id) is not TodoList list))
        {
            return new Result<TodoList>() { Message = NotFound<TodoList>(), ResultType = ResultType.NotFound };
        }
        else if (list.Todos.Any(t => !t.IsComplete))
        {
            return new Result<TodoList>() { Message = CANNOT_DELETE_TODO_LISTS_WITH_TODOS_NOT_COMPLETE, ResultType = ResultType.Error };
        }
        else
        {
            TodoLists.Remove(list);
            await SaveChangesAsync();
            return new Result<TodoList>() { Message = DELETED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> DeleteTodoAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else if (todo.IsImportant)
        {
            return new Result<Todo>() { Message = CANNOT_DELETE_IMPORTANT_TODOS, ResultType = ResultType.Error };
        }
        else
        {
            Todos.Remove(todo);
            await SaveChangesAsync();
            return new Result<Todo>() { Message = DELETED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<TodoList>> RenameTodoListAsync(Guid id, RenameTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if ((await GetListDetailAsync(id)) is not TodoList list)
        {
            return new Result<TodoList>() { Message = NotFound<TodoList>(), ResultType = ResultType.NotFound };
        }
        else if (!vr.IsValid)
        {
            return new Result<TodoList>() { Message = FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else if (list.Name != dto.Name)
        {
            list.Name = dto.Name;
            await SaveChangesAsync();
        }
        return new Result<TodoList>() { Message = RENAMED_SUCCESSFULLY, ResultType = ResultType.Success };
    }    
    internal async Task<Result<Todo>> UpdateTodoAsync(Guid id, UpdateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else if (!vr.IsValid)
        {
            return new Result<Todo>() { Message = FormattedValidationErrors(vr.Errors), ResultType = ResultType.Invalid };
        }
        else
        {
            if (todo.Description != dto.Description) { todo.Description = dto.Description; }
            if (todo.DueDate != dto.DueDate) { todo.DueDate = dto.DueDate; }
            if (todo.IsImportant != dto.IsImportant) { todo.IsImportant = dto.IsImportant; }
            if (todo.IsComplete != dto.IsComplete) { todo.IsComplete = dto.IsComplete; }
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }

            await SaveChangesAsync();
            return new Result<Todo>() { Message = UPDATED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> ToggleTodoImportanceAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            todo.IsImportant = !todo.IsImportant;
            await SaveChangesAsync();
            return new Result<Todo>() { Message = TOGGLED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> ToggleTodoCompletionAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            todo.IsComplete = !todo.IsComplete;
            await SaveChangesAsync();
            return new Result<Todo>() { Message = TOGGLED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
    internal async Task<Result<Todo>> MoveTodoToListAsync(Guid id, UpdateTodo dto)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return new Result<Todo>() { Message = NotFound<Todo>(), ResultType = ResultType.NotFound };
        }
        else
        {
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }
            await SaveChangesAsync();
            return new Result<Todo>() { Message = MOVED_SUCCESSFULLY, ResultType = ResultType.Success };
        }
    }
}
