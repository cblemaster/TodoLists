using Microsoft.EntityFrameworkCore;
using TodoLists.Server.DatabaseContexts;
using TodoLists.Server.Entities;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();

string connectionString = config.GetConnectionString("Project") ?? "Error retrieving connection string!";

builder.Services.AddDbContext<TodoListsContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/", () => "Welcome to Todo Lists!");

app.MapPost("/todolist", async (TodoListsContext context, DTO dto) =>
{
    TodoList todoList = new() {
        TodoListId = dto.TodoListId,
        Name = dto.Name,
    };
    context.TodoLists.Add(todoList);
    await context.SaveChangesAsync();
    return todoList;
});
app.MapPut("/todolist/{id:int}", async (TodoListsContext context, int id, DTO dto) =>
{
    if ((await context.TodoLists.FindAsync(id)) is not TodoList entity) {
        return NotFound();
    }
    if (entity.Name != dto.Name) {
        entity.Name = dto.Name;
    }
    await context.SaveChangesAsync();
    return NoContent();
});
app.MapDelete("/todolist/{id:int}", async (TodoListsContext context, int id) =>
{
    if ((await context.TodoLists.Include(list => list.Todos).FindAsync(id)) is not TodoList todoList) {
        return NotFound();
    }
    if (todoList.Todos.Count > 0) {
        return BadRequest("Delete the todos in this todo list before deleting the todo list");
    }
    context.TodoLists.Remove(todoList);
    await context.SaveChangesAsync();
    return NoContent();
});
app.MapGet("/todolist", async (TodoListsContext context) =>
{
    return context.TodoLists.OrderBy(list => list.Name).AsNoTracking();
});
app.MapGet("/todolist/{id:int}", async (TodoListsContext context, int id) =>
{
    if (context.TodoLists
        .Include(list => list.Todos)
        .Where(list => list.TodoListId == id)
        .OrderBy(list => list.Name)
        .AsNoTracking()
        .AsAsyncEnumerable()
        is IEnumerable<Todo> todoLists) {
            
        return todoLists;
    }
    return NotFound();
});

app.MapPost("/todo", async (TodoListsContext context, DTO dto) =>
{
    Todo todo = new() {
        TodoListId = dto.TodoListId,
        Description = dto.Description,
        DueDate = dto.DueDate,
        IsImportant = dto.IsImportant,
        IsComplete = dto.IsComplete,
    };
    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    return todo;
});
app.MapPut("/todo/{id:int}", async (TodoListsContext context, int id, DTO dto) =>
{
    if ((await context.Todos.FindAsync(id)) is not Todo entity) {
        return NotFound();
    }
    if (entity.Description != dto.Description) {
        entity.Description = dto.Description;
    }
    if (entity.DueDate != dto.DueDate) {
        entity.DueDate = dto.DueDate;
    }
    if (entity.IsImportant != dto.IsImportant) {
        entity.IsImportant = dto.IsImportant;
    }
    if (entity.IsComplete != dto.IsComplete) {
        entity.IsComplete = dto.IsComplete;
    }
    await context.SaveChangesAsync();
    return NoContent();
});
app.MapDelete("/todo/{id:int}", async (TodoListsContext context, int id) =>
{
    if ((await context.Todos.FindAsync(id)) is not Todo todo) {
        return NotFound();
    }
    context.Todos.Remove(todo);
    await context.SaveChangesAsync();
    return NoContent();
});
app.MapGet("/todolist/{id:int}/todo", async (TodoListsContext context, int id) =>
{
    if (context.Todos
        .Where(todo => todo.TodoListId == id)
        .OrderByDescending(todo => todo.DueDate)
        .ThenBy(todo => todo.Description)
        .AsNoTracking()
        .AsAsyncEnumerable()
        is IEnumerable<Todo> todos) {
        
        return todos;
    }
    return NotFound();
});

//app.MapFallbackToFile("/index.html");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
