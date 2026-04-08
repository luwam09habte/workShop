// ServerAPI/Program.cs

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register your services
builder.Services.AddSingleton<ServerAPI.Service.UserService>();
builder.Services.AddSingleton<ServerAPI.Service.ClothingService>();

// Allow requests from the Blazor WASM app
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5168",
                "https://localhost:7125"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("BlazorClient"); // must be before UseAuthorization and MapControllers

app.UseAuthorization();
app.MapControllers();

app.Run();