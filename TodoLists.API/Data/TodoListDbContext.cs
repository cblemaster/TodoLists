
using Microsoft.EntityFrameworkCore;
using TodoLists.API.Data.Models;
using TodoLists.API.Validation;
using TodoLists.Domain;

namespace TodoLists.API.Data;

internal class TodoListDbContext : DbContext
{
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

    internal async Task<TodoList> CreateTodoListAsync(CreateTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if (!vr.IsValid)
        {
            return TodoList.NotValid(vr.Errors);
        }
        else
        {
            await TodoLists.AddAsync(new TodoList() { Id = dto.Id, Name = dto.Name });
            await SaveChangesAsync();
            return TodoList.NoContent();
        }
    }

    internal async Task<Todo> CreateTodoAsync(CreateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if (!vr.IsValid)
        {
            return Todo.NotValid(vr.Errors);
        }
        else
        {
            await Todos.AddAsync(new Todo() { Id = dto.Id, Description = dto.Description });
            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }

    internal async Task<TodoList> DeleteTodoListAsync(Guid id)
    {
        if ((await GetListDetailAsync(id) is not TodoList list))
        {
            return TodoList.NotFound();
        }
        else if (list.Todos.Any(t => !t.IsComplete))
        {
            return TodoList.Error("Cannot delete todo list because it contains todos that are not complete.");
        }
        else
        {
            TodoLists.Remove(list);
            await SaveChangesAsync();
            return TodoList.NoContent();
        }
    }
    internal async Task<Todo> DeleteTodoAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return Todo.NotFound();
        }
        else if (todo.IsImportant)
        {
            return Todo.Error("Cannot delete important todos.");
        }
        else
        {
            Todos.Remove(todo);
            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }

    internal async Task<TodoList> RenameTodoListAsync(Guid id, RenameTodoList dto)
    {
        ValidationResult vr = dto.Name.ValidateTodoListName();
        if ((await GetListDetailAsync(id)) is not TodoList list)
        {
            return TodoList.NotFound();
        }
        else if (!vr.IsValid)
        {
            return TodoList.NotValid(vr.Errors);
        }
        else if (list.Name != dto.Name)
        {
            list.Name = dto.Name;
            await SaveChangesAsync();
        }        
        return TodoList.NoContent();
    }
    
    internal async Task<Todo> UpdateTodoAsync(Guid id, UpdateTodo dto)
    {
        ValidationResult vr = dto.Description.ValidateTodoDescription();
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return Todo.NotFound();
        }
        else if (!vr.IsValid)
        {
            return Todo.NotValid(vr.Errors);
        }
        else
        {
            if (todo.Description != dto.Description) { todo.Description = dto.Description; }
            if (todo.DueDate != dto.DueDate) { todo.DueDate = dto.DueDate; }
            if (todo.IsImportant != dto.IsImportant) { todo.IsImportant = dto.IsImportant; }
            if (todo.IsComplete != dto.IsComplete) { todo.IsComplete = dto.IsComplete; }
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }

            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }

    internal async Task<Todo> ToggleTodoImportanceAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return Todo.NotFound();
        }
        else
        {
            todo.IsImportant = !todo.IsImportant;
            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }

    internal async Task<Todo> ToggleTodoCompletionAsync(Guid id)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return Todo.NotFound();
        }
        else
        {
            todo.IsComplete = !todo.IsComplete;
            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }

    internal async Task<Todo> MoveTodoToListAsync(Guid id, UpdateTodo dto)
    {
        if ((await GetTodoAsync(id)) is not Todo todo)
        {
            return Todo.NotFound();
        }
        else
        {
            if (todo.TodoListId != dto.TodoListId) { todo.TodoListId = dto.TodoListId; }
            await SaveChangesAsync();
            return Todo.NoContent();
        }
    }
}
