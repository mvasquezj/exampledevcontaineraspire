namespace TodoApi.Models;

public sealed class TodoItem
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsComplete { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}