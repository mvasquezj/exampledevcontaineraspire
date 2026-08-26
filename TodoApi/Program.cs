using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("my-app-db")
    ?? throw new InvalidOperationException("Connection string 'my-app-db' was not found.");

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapPost("/todos", async (CreateTodoRequest request, TodoDbContext dbContext) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["title"] = ["Title is required."]
        });
    }

    var todo = new TodoItem
    {
        Title = request.Title.Trim()
    };

    dbContext.Todos.Add(todo);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapGet("/todos/{id:int}", async (int id, TodoDbContext dbContext) =>
{
    var todo = await dbContext.Todos.FindAsync(id);
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.MapGet("/todos", async (TodoDbContext dbContext) =>
    await dbContext.Todos.AsNoTracking().OrderBy(todo => todo.Id).ToListAsync());

app.Run();

public sealed record CreateTodoRequest(string Title);