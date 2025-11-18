namespace DependencyInjectionDemo.Controllers;

using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;
using DependencyInjectionDemo.Services.Gateways;
using DependencyInjectionDemo.Services.Validators;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IKeyedServiceProvider keyedServices;
    private readonly IEnumerable<IPaymentValidator> validators;

    public PaymentsController(
        IKeyedServiceProvider keyedServices,
        IEnumerable<IPaymentValidator> validators)
    {
        this.keyedServices = keyedServices;
        this.validators = validators;
    }

    [HttpPost("charge/{provider}")]
    public async Task<ActionResult<PaymentResult>> Charge(
        string provider,
        decimal amount,
        string currency = "USD")
    {
        // Way 1. All validators
        foreach (var validator in this.validators)
        {
            var result = await validator.ValidateAsync(amount, currency, provider);
            if (!result.IsValid)
                return this.BadRequest(new PaymentResult(false, provider, ErrorMessage: result.ErrorMessage));
        }

        // Way 2. Payment with keyed gateway
        var gateway = this.keyedServices
            .GetRequiredKeyedService<IPaymentGateway>(provider.ToLowerInvariant());
        var payment = await gateway.ProcessAsync(amount, currency);

        return this.Ok(payment);
    }

    [HttpPost("charge-factory")]
    public async Task<ActionResult<PaymentResult>> ChargeWithFactory(
    [FromBody] ChargeWithFactoryRequest request)
    {
        // 1. Walidacja – wszystkie walidatory działają tak samo
        foreach (var validator in this.validators)
        {
            var validation = await validator.ValidateAsync(
                request.Amount,
                request.Currency,
                request.Provider.ToString()); // enum → string do walidatora

            if (!validation.IsValid)
                return this.BadRequest(new PaymentResult(
                    false,
                    request.Provider.ToString(),
                    ErrorMessage: validation.ErrorMessage));
        }

        // 2. Płatność – teraz przez fabrykę! (dodamy ją za chwilę)
        // Najpierw pobieramy fabrykę z IServiceProvider (bo nie mamy jej w konstruktorze)
        var factory = this.HttpContext.RequestServices
            .GetRequiredService<PaymentGatewayFactory>();

        var gateway = factory.Create(request.Provider);
        var payment = await gateway.ProcessAsync(request.Amount, request.Currency);

        return this.Ok(payment);
    }
}