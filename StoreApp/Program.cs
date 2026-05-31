using Serilog;
using StoreApp.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/storeapp-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1)
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<CategoryService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();