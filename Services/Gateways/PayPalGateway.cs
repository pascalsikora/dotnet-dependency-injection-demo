namespace DependencyInjectionDemo.Services.Gateways;

using DependencyInjectionDemo.Models;

public class PayPalGateway : IPaymentGateway
{
    public Task<PaymentResult> ProcessAsync(decimal amount, string currency)
        => Task.FromResult(new PaymentResult(true, "PayPal", Guid.NewGuid().ToString("N")[..12]));
}