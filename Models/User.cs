using System;
namespace DependencyInjectionDemo.Models;

public record User(
    string FirstName,
    string LastName,
    string Email)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}