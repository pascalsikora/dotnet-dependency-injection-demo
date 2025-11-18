namespace DependencyInjectionDemo.Services.Gateways;

using DependencyInjectionDemo.Models;


public class StripeGateway : IPaymentGateway
{
    private readonly ILogger<StripeGateway> logger;

    public StripeGateway(ILogger<StripeGateway> logger) => this.logger = logger;

    public Task<PaymentResult> ProcessAsync(decimal amount, string currency)
    {
        this.logger.LogInformation("Stripe: charging {Amount} {Currency}", amount, currency);
        return Task.FromResult(new PaymentResult(true, "Stripe", Guid.NewGuid().ToString("N")[..10]));
    }
}