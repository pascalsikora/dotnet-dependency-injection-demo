namespace DependencyInjectionDemo.Services.Gateways;

using DependencyInjectionDemo.Models;

public class CryptoGateway : IPaymentGateway
{
    public Task<PaymentResult> ProcessAsync(decimal amount, string currency)
        => Task.FromResult(new PaymentResult(true, $"Crypto-{currency}"));
}