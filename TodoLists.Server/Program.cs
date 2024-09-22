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
