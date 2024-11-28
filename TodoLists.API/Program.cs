
using Microsoft.EntityFrameworkCore;
using TodoLists.API.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();
string connectionString = configRoot.GetConnectionString("Project") ?? "Error retrieving connection string!";
builder.Services.AddDbContext<TodoListDbContext>(options => options.UseSqlServer(connectionString));
WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.Run();
