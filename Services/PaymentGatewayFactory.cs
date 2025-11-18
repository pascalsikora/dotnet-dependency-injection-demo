using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services.Gateways;

namespace DependencyInjectionDemo.Services;

public class PaymentGatewayFactory
{
    private readonly IKeyedServiceProvider keyedServices;

    public PaymentGatewayFactory(IKeyedServiceProvider keyedServices)
    {
        this.keyedServices = keyedServices ?? throw new ArgumentNullException(nameof(keyedServices));
    }

    public IPaymentGateway Create(PaymentProvider provider) => provider switch
    {
        PaymentProvider.Stripe => this.keyedServices.GetRequiredKeyedService<IPaymentGateway>("stripe"),
        PaymentProvider.PayPal => this.keyedServices.GetRequiredKeyedService<IPaymentGateway>("paypal"),
        PaymentProvider.Crypto => this.keyedServices.GetRequiredKeyedService<IPaymentGateway>("crypto"),
        _ => throw new NotSupportedException($"Payment provider {provider} is not supported.")
    };
}