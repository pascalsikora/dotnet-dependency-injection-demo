namespace DependencyInjectionDemo.Services.Validators;

using DependencyInjectionDemo.Models;

public class CreditCardValidator : IPaymentValidator
{
    private readonly ILogger<CreditCardValidator> logger;

    public CreditCardValidator(ILogger<CreditCardValidator> logger) => this.logger = logger;

    public Task<ValidationResult> ValidateAsync(decimal amount, string currency, string provider)
    {
        if (amount <= 0) return Task.FromResult(ValidationResult.Fail("Amount must be positive"));
        if (amount > 50_000m) return Task.FromResult(ValidationResult.Fail("Card limit exceeded"));
        if (!new[] { "USD", "EUR", "GBP" }.Contains(currency))
            return Task.FromResult(ValidationResult.Fail("Currency not supported for cards"));

        return Task.FromResult(ValidationResult.Success);
    }
}