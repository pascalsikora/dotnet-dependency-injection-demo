namespace DependencyInjectionDemo.Services.Validators;

using DependencyInjectionDemo.Models;

public class CryptoAmountValidator : IPaymentValidator
{
    public Task<ValidationResult> ValidateAsync(decimal amount, string currency, string provider)
        => amount < 0.01m
            ? Task.FromResult(ValidationResult.Fail("Minimum crypto amount is 0.01"))
            : Task.FromResult(ValidationResult.Success);
}