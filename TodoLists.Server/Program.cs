using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoLists.Server;
using TodoLists.Server.DataTransferObjects;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .Build();

string connectionString = config.GetConnectionString("Project") ?? "Error retrieving connection string!";

builder.Services
    .AddDbContext<TodoListsContext>(options => options.UseSqlServer(connectionString))
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

WebApplication app = builder.Build();

app.UseDefaultFiles().UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/todolist", async Task<Results<BadRequest<string>, Ok<GetTodoList>>>
            (TodoListsContext context, CreateTodoList dto) =>
{
    (bool IsValid, string ErrorMessage) = dto.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }
    TodoList todoList = new()
    {
        Name = dto.Name,
    };
    context.TodoLists.Add(todoList);
    await context.SaveChangesAsync();
    return TypedResults.Ok(new GetTodoList(todoList.TodoListId, todoList.Name, []));
})
            .WithOpenApi();
app.MapPost("/todo", async Task<Results<BadRequest<string>, Ok<GetTodo>>> (TodoListsContext context, CreateTodo dto) =>
{
    (bool IsValid, string ErrorMessage) = dto.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }
    Todo todo = new()
    {
        TodoListId = dto.TodoListId,
        Description = dto.Description,
        DueDate = dto.DueDate,
        IsImportant = dto.IsImportant,
        IsComplete = dto.IsComplete,
    };
    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    return TypedResults.Ok(new GetTodo(todo.TodoId, todo.TodoListId, todo.Description, todo.DueDate,
        todo.IsImportant, todo.IsComplete));
})
.WithOpenApi();
app.MapGet("/todolist", Ok<IEnumerable<GetTodoList>> (TodoListsContext context) =>
{
    List<GetTodoList> returnList = [];
    List<TodoList> lists = [.. context.TodoLists.OrderBy(list => list.Name).AsNoTracking()];
    lists.ForEach(list => returnList.Add(new(list.TodoListId, list.Name, [])));
    return TypedResults.Ok(returnList.AsEnumerable());
})
    .WithOpenApi();
app.MapGet("/todolist/{id:int}", async Task<Results<NotFound, Ok<GetTodoList>>> (TodoListsContext context, int id) =>
{
    TodoList list = (await context.TodoLists.Include(list => list.Todos).AsNoTracking()
        .SingleOrDefaultAsync(list => list.TodoListId == id))!;
    return list is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(new GetTodoList(list.TodoListId, list.Name, MapTodoEntityToDTO(list.Todos)));
})
.WithOpenApi();
app.MapGet("/todo/{id:int}", async Task<Results<Ok<GetTodo>, NotFound>> (TodoListsContext context, int id) =>
    await context.Todos.AsNoTracking()
        .SingleOrDefaultAsync(todo => todo.TodoId == id)
        is Todo todo
        ? TypedResults.Ok(new GetTodo(todo.TodoId, todo.TodoListId, todo.Description, todo.DueDate,
            todo.IsImportant, todo.IsComplete))
        : TypedResults.NotFound())
.WithOpenApi();
app.MapPut("/todolist/{id:int}", async Task<Results<NotFound, BadRequest<string>,
    NoContent>> (TodoListsContext context, int id, UpdateTodoList dto) =>
{
    if ((await context.TodoLists.FindAsync(id)) is not TodoList entity)
    {
        return TypedResults.NotFound();
    }
    (bool IsValid, string ErrorMessage) = dto.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }
    if (entity.Name != dto.Name)
    {
        entity.Name = dto.Name;
    }
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
})
    .WithOpenApi();
app.MapPut("/todo/{id:int}", async Task<Results<NotFound, BadRequest<string>, NoContent>>
    (TodoListsContext context, int id, UpdateTodo dto) =>
{
    if ((await context.Todos.FindAsync(id)) is not Todo entity)
    {
        return TypedResults.NotFound();
    }
    (bool IsValid, string ErrorMessage) = dto.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }
    if (entity.Description != dto.Description)
    {
        entity.Description = dto.Description;
    }
    if (entity.DueDate != dto.DueDate)
    {
        entity.DueDate = dto.DueDate;
    }
    if (entity.IsImportant != dto.IsImportant)
    {
        entity.IsImportant = dto.IsImportant;
    }
    if (entity.IsComplete != dto.IsComplete)
    {
        entity.IsComplete = dto.IsComplete;
    }
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
})
    .WithOpenApi();
app.MapDelete("/todolist/{id:int}", async Task<Results<NotFound, BadRequest<string>, NoContent>>
    (TodoListsContext context, int id) =>
{
    if ((await context.TodoLists
        .Include(list => list.Todos)
        .SingleOrDefaultAsync(list => list.TodoListId == id)
        is not TodoList todoList))
    {
        return TypedResults.NotFound();
    }
    if (todoList.Todos.Count > 0)
    {
        return TypedResults.BadRequest("Delete the todos in this todo list before deleting the todo list");
    }
    context.TodoLists.Remove(todoList);
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
})
    .WithOpenApi();
app.MapDelete("/todo/{id:int}", async Task<Results<NotFound, NoContent>> (TodoListsContext context, int id) =>
{
    if ((await context.Todos.FindAsync(id)) is not Todo todo)
    {
        return TypedResults.NotFound();
    }
    context.Todos.Remove(todo);
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
})
.WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();

static IEnumerable<GetTodo> MapTodoEntityToDTO(ICollection<Todo> todos)
{
    List<GetTodo> dtos = [];
    todos.ToList().ForEach(todo => dtos.Add(new(todo.TodoId, todo.TodoListId, todo.Description,
        todo.DueDate, todo.IsImportant, todo.IsComplete)));
    return dtos.AsEnumerable();
}
