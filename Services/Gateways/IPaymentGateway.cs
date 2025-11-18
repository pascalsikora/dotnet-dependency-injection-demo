
namespace DependencyInjectionDemo.Services.Gateways;

using DependencyInjectionDemo.Models;

public interface IPaymentGateway
{
    Task<PaymentResult> ProcessAsync(decimal amount, string currency);
}