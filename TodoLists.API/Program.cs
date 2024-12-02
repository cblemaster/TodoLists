
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoLists.API.Data;
using TodoLists.API.Data.Models;
using TodoLists.Domain;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();
string connectionString = configRoot.GetConnectionString("Project") ?? "Error retrieving connection string!";
builder.Services.AddDbContext<TodoListDbContext>(options => options.UseSqlServer(connectionString));
WebApplication app = builder.Build();

// TODO >> endpoint tests, look up best way to do this; want something easily repeatable

// queries
app.MapGet("/", () => "Welcome to Todo Lists!");
app.MapGet("/listsummaries", Results<NotFound<string>, Ok<IEnumerable<ListSummary>>> (TodoListDbContext context) =>
{
    IEnumerable<ListSummary> summaries = context.GetListSummaries();
    return !summaries.Any() ? TypedResults.NotFound("No summaries found.") : TypedResults.Ok(summaries);
});
app.MapGet("/listdetail/{id:guid}", async Task<Results<NotFound<string>, Ok<ListDetail>>> (TodoListDbContext context, Guid id) =>
{
    ListDetail? list = await (context.GetListAsync(id));
    return list is null ? TypedResults.NotFound("List detail not found.") : TypedResults.Ok(list);
});
app.MapGet("/todosummary/{id:guid}", async Task<Results<NotFound<string>, Ok<TodoSummary>>> (TodoListDbContext context, Guid id) =>
{
    TodoSummary? summary = await context.GetTodoSummaryAsync(id);
    return summary is null ? TypedResults.NotFound("Todo summary not found.") : TypedResults.Ok(summary);
});
app.MapGet("/todo/{id:guid}", async Task<Results<NotFound<string>, Ok<Todo>>> (TodoListDbContext context, Guid id) =>
{
    Todo? todo = await context.GetTodoEntityAsync(id);
    return todo is null ? TypedResults.NotFound("Todo not found.") : TypedResults.Ok(todo);
});
app.MapGet("/todo/duetoday", Results<NotFound<string>, Ok<IEnumerable<TodoSummary>>> (TodoListDbContext context) =>
{
    IEnumerable<TodoSummary> dueToday = context.GetTodosDueToday();
    return !dueToday.Any() ? TypedResults.NotFound("No todos due today found.") : TypedResults.Ok(dueToday);
});
app.MapGet("/todo/important", Results<NotFound<string>, Ok<IEnumerable<TodoSummary>>> (TodoListDbContext context) =>
{
    IEnumerable<TodoSummary> important = context.GetImportantTodos();
    return !important.Any() ? TypedResults.NotFound("No important todos found.") : TypedResults.Ok(important);
});
app.MapGet("/todo/complete", Results<NotFound<string>, Ok<IEnumerable<TodoSummary>>> (TodoListDbContext context) =>
{
    IEnumerable<TodoSummary> complete = context.GetCompletedTodos();
    return !complete.Any() ? TypedResults.NotFound("No completed todos found.") : TypedResults.Ok(complete);
});

// commands
app.MapPost("/list", async Task<Results<ValidationProblem, Created<ListSummary>, ProblemHttpResult>> (TodoListDbContext context, CreateTodoList list) =>
{
    TodoLists.API.Data.Results.Result<ListSummary> result = await context.CreateTodoListAsync(list);
    if (result.ResultType == TodoLists.API.Data.Results.ResultType.Invalid)
    {
        string key = "validationErrors";
        string[] errors = [result.Message];
        KeyValuePair<string, string[]> kvp = new(key, errors);
        return TypedResults.ValidationProblem(new Dictionary<string, string[]>([kvp]));
    }
    else
    {
        return result.ResultType == TodoLists.API.Data.Results.ResultType.Success
            ? TypedResults.Created($"/listdetail/{result.Payload.Id}", result.Payload)
            : TypedResults.Problem("An unknown error occured creating the list.");
    }
});
app.MapPost("/todo", async Task<Results<ValidationProblem, Created<TodoSummary>, ProblemHttpResult>> (TodoListDbContext context, CreateTodo todo) =>
{
    TodoLists.API.Data.Results.Result<TodoSummary> result = await context.CreateTodoAsync(todo);
    if (result.ResultType == TodoLists.API.Data.Results.ResultType.Invalid)
    {
        string key = "validationErrors";
        string[] errors = [result.Message];
        KeyValuePair<string, string[]> kvp = new(key, errors);
        return TypedResults.ValidationProblem(new Dictionary<string, string[]>([kvp]));
    }
    else
    {
        return result.ResultType == TodoLists.API.Data.Results.ResultType.Success
            ? TypedResults.Created($"/todosummary/{result.Payload.Id}", result.Payload)
            : TypedResults.Problem("An unknown error occured creating the todo.");
    }
});
app.MapDelete("/list/{id:guid}", async Task<Results<NotFound<string>, ProblemHttpResult, NoContent>> (TodoListDbContext context, Guid id) =>
{
    TodoLists.API.Data.Results.Result<TodoList> result = await context.DeleteTodoListAsync(id);
    return result.ResultType switch
    {
        TodoLists.API.Data.Results.ResultType.NotFound => TypedResults.NotFound("Todo list not found."),
        TodoLists.API.Data.Results.ResultType.Error => TypedResults.Problem(result.Message),
        _ => result.ResultType == TodoLists.API.Data.Results.ResultType.Success
            ? TypedResults.NoContent()
            : TypedResults.Problem("An unknown error occured deleting the list.")
    };
});
app.MapDelete("/todo/{id:guid}", async Task<Results<NotFound<string>, ProblemHttpResult, NoContent>> (TodoListDbContext context, Guid id) =>
{
    TodoLists.API.Data.Results.Result<Todo> result = await context.DeleteTodoAsync(id);
    return result.ResultType switch
    {
        TodoLists.API.Data.Results.ResultType.NotFound => TypedResults.NotFound("Todo not found."),
        TodoLists.API.Data.Results.ResultType.Error => TypedResults.Problem(result.Message),
        _ => result.ResultType == TodoLists.API.Data.Results.ResultType.Success
            ? TypedResults.NoContent()
            : TypedResults.Problem("An unknown error occured deleting the todo.")
    };
});
app.MapPut("/list/{id:guid}/rename", async Task<Results<NotFound<string>, ValidationProblem, NoContent, ProblemHttpResult>> (TodoListDbContext context, Guid id, RenameTodoList dto) =>
{
    TodoLists.API.Data.Results.Result<TodoList> result = await context.RenameTodoListAsync(id, dto);
    switch (result.ResultType)
    {
        case TodoLists.API.Data.Results.ResultType.NotFound:
            return TypedResults.NotFound("Todo list not found.");
        case TodoLists.API.Data.Results.ResultType.Invalid:
            {
                string key = "validationErrors";
                string[] errors = [result.Message];
                KeyValuePair<string, string[]> kvp = new(key, errors);
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>([kvp]));
            }
        case TodoLists.API.Data.Results.ResultType.Success:
            return TypedResults.NoContent();
        default:
            return TypedResults.Problem("An unknown error occured renaming the list.");
    }
});
app.MapPut("/todo/{id:guid}", async Task<Results<NotFound<string>, ValidationProblem, NoContent, ProblemHttpResult>> (TodoListDbContext context, Guid id, UpdateTodo dto) =>
{
    TodoLists.API.Data.Results.Result<Todo> result = await context.UpdateTodoAsync(id, dto);
    switch (result.ResultType)
    {
        case TodoLists.API.Data.Results.ResultType.NotFound:
            return TypedResults.NotFound("Todo not found.");
        case TodoLists.API.Data.Results.ResultType.Invalid:
            {
                string key = "validationErrors";
                string[] errors = [result.Message];
                KeyValuePair<string, string[]> kvp = new(key, errors);
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>([kvp]));
            }

        case TodoLists.API.Data.Results.ResultType.Success:
            return TypedResults.NoContent();
        default:
            return TypedResults.Problem("An unknown error occured updating the todo.");
    }
});
app.MapPut("/todo/{id:guid}/importance", async Task<Results<NotFound<string>, NoContent, ProblemHttpResult>> (TodoListDbContext context, Guid id) =>
{
    TodoLists.API.Data.Results.Result<Todo> result = await context.ToggleTodoImportanceAsync(id);
    return result.ResultType switch
    {
        TodoLists.API.Data.Results.ResultType.NotFound => TypedResults.NotFound("Todo not found."),
        TodoLists.API.Data.Results.ResultType.Success => TypedResults.NoContent(),
        _ => TypedResults.Problem("An unknown error occured toggling the todo's importance.")
    };
});
app.MapPut("/todo/{id:guid}/completed", async Task<Results<NotFound<string>, NoContent, ProblemHttpResult>> (TodoListDbContext context, Guid id) =>
{
    TodoLists.API.Data.Results.Result<Todo> result = await context.ToggleTodoCompletionAsync(id);
    return result.ResultType switch
    {
        TodoLists.API.Data.Results.ResultType.NotFound => TypedResults.NotFound("Todo not found."),
        TodoLists.API.Data.Results.ResultType.Success => TypedResults.NoContent(),
        _ => TypedResults.Problem("An unknown error occured toggling the todo's completion.")
    };
});
app.MapPut("/todo/{id:guid}/movetolist", async Task<Results<NotFound<string>, NoContent, ProblemHttpResult>> (TodoListDbContext context, Guid id, UpdateTodo dto) =>
{
    TodoLists.API.Data.Results.Result<Todo> result = await context.MoveTodoToListAsync(id, dto);
    return result.ResultType switch
    {
        TodoLists.API.Data.Results.ResultType.NotFound => TypedResults.NotFound("Todo not found."),
        TodoLists.API.Data.Results.ResultType.Success => TypedResults.NoContent(),
        _ => TypedResults.Problem("An unknown error occured moving the todo to a different list.")
    };
});

app.Run();
