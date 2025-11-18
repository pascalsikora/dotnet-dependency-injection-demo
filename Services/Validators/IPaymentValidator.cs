namespace DependencyInjectionDemo.Services.Validators;

using DependencyInjectionDemo.Models;

public interface IPaymentValidator
{
    Task<ValidationResult> ValidateAsync(decimal amount, string currency, string provider);
}