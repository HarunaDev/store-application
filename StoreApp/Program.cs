using Serilog;
using StoreApp.Services;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<PasswordService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:SecretKey"]!))
        };
});


var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!").ExcludeFromDescription();

app.Run();