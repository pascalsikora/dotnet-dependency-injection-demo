namespace DependencyInjectionDemo.Models;

public record PaymentResult(
    bool Success,
    string Gateway,
    string? TransactionId = null,
    string? ErrorMessage = null);