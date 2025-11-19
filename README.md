

# Dependency Injection Demo in Modern .NET

> This code is a complete, up-to-date introduction to DI in .NET for developers who already know C# but want to master the current best practices.


## How to start similar project
```powershell
# Install dotnet SDK - if it's needed
winget install Microsoft.DotNet.SDK.9

# Create minimal APIs style (recommended in 2025)
dotnet new web -n DependencyInjectionDemo

# Or create new Web API project
dotnet new webapi -n DependencyInjectionDemo --use-controllers

# Go to Your project
cd DependencyInjectionDemo

# For Demo purposestat
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory

# Swagger API
dotnet add package Swashbuckle.AspNetCore

# Run your GUI
start DependencyInjectionDemo.csproj
```
## Modern Dependency Injection in .NET – Demo Project 

![.NET 9](https://img.shields.io/badge/.NET-9-blueviolet?logo=dotnet)
![C# 14](https://img.shields.io/badge/C%23-14-brightgreen)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)

Fully working, production-ready example of **advanced Dependency Injection** in .NET – built exactly the way to show how it can be coded in various approaches.

## What you will learn

| Feature                              | Best Practice Used                                   |
|--------------------------------------|--------------------------------------------------------------------|
| Records & primary constructors       | Immutable-first models                                            |
| Keyed Services (.NET 8+)             | `AddKeyedSingleton`, `IKeyedServiceProvider`                     |
| Strongly-typed factory + enum        | No magic strings – `PaymentProvider` enum + `PaymentGatewayFactory` |
| Multiple validator implementations   | Injected as `IEnumerable<IPaymentValidator>`                      |
| Swagger with dropdowns               | `[JsonStringEnumConverter]` + `[EnumMember]`                      |
| Clean startup (`Program.cs`)         | Proper service registration order                                 |
| In-memory EF Core + seeding          | Instant startup, perfect for demos                                |

## Endpoints

### Users
- `GET    /api/users` → list all users  
- `POST   /api/users` → create user (returns `Location` header)  
- `GET    /api/users/{id}` → get by ID

### Payments (two styles!)

#### Classic style (string-based)
```http
POST /api/payments/charge/stripe?amount=99.99
POST /api/payments/charge/PayPal?amount=22.50
```

#### Modern style – factory + enum (recommended)
```http
POST /api/payments/charge-factory
{
  "amount": 149.99,
  "provider": "crypto",
  "currency": "USD"
}
```

Swagger shows beautiful dropdown: `Stripe ▼ PayPal ▼ Crypto`

## Project Highlights (2025 edition)

- Zero `new` in business logic
- Zero magic strings in payment routing
- Full testability (all dependencies injected)
- Ready for Clean Architecture / Vertical Slices
- Perfect template for job interviews 


## License

MIT © 2025 – feel free to fork, star, and use in interviews!

Made with love for the .NET community in 2025. Happy injecting!