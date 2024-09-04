using TodoLists.Server.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = builder.BuildConfigurationRoot();
builder.ConfigureAndRegisterServices(configRoot);
WebApplication app =
    builder.Build()
        .ConfigureWebApplication()
        .MapApiEndpoints();
app.MapFallbackToFile("/index.html");
app.Run();
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
