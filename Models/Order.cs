namespace DependencyInjectionDemo.Models;

public record Order(
    Guid UserId,
    decimal Amount,
    string Currency = "USD")
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}