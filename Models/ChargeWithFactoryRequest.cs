namespace DependencyInjectionDemo.Models;

public record ChargeWithFactoryRequest(
    decimal Amount,
    PaymentProvider Provider,
    string Currency = "USD"
);