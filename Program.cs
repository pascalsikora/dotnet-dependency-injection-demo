using DependencyInjectionDemo.Data;
using DependencyInjectionDemo.Interfaces;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;
using DependencyInjectionDemo.Services.Gateways;
using DependencyInjectionDemo.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionDemo.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ========================================================================
        // 1. Service registration – everything must be registered BEFORE Build()
        // ========================================================================

        // MVC Controllers + API explorer (required for MapControllers & Swagger)
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Required for resolving keyed services via IKeyedServiceProvider (.NET 8+)
        builder.Services.AddSingleton<IKeyedServiceProvider>(sp => (IKeyedServiceProvider)sp);

        // Entity Framework Core – InMemory database (perfect for demos & tests)
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("DependencyInjectionDemo"));

        // Application services
        builder.Services.AddScoped<IUserService, UserService>();

        // Multiple validator implementations – will be injected as IEnumerable<IPaymentValidator>
        builder.Services.AddSingleton<IPaymentValidator, CreditCardValidator>();
        builder.Services.AddSingleton<IPaymentValidator, CryptoAmountValidator>();

        // Keyed services – modern .NET 8+ way to resolve implementations by key
        builder.Services.AddKeyedSingleton<IPaymentGateway, StripeGateway>("stripe");
        builder.Services.AddKeyedSingleton<IPaymentGateway, PayPalGateway>("paypal");
        builder.Services.AddKeyedSingleton<IPaymentGateway, CryptoGateway>("crypto");

        // Payment Factory Service - Strongly-typed factory 
        builder.Services.AddSingleton<PaymentGatewayFactory>();

        // ========================================================================
        // 2. Build the application
        // ========================================================================
        var app = builder.Build();

        // ========================================================================
        // 3. Middleware pipeline configuration
        // ========================================================================
        app.UseSwagger();
        app.UseSwaggerUI();

        // Map attribute-routed controllers (UsersController, PaymentsController, etc.)
        app.MapControllers();

        // ========================================================================
        // 4. Seed initial data (executed once on startup)
        // ========================================================================
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User("Ada", "Lovelace", "ada@example.com"),
                    new User("Grace", "Hopper", "grace@example.com")
                );
                db.SaveChanges();
            }
        }

        // ========================================================================
        // 5. Run the application
        // ========================================================================
        app.Run();
    }
}