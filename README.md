

# Dependency Injection Demo in Modern .NET


If you’re a .NET developer in 2025, you already know that Dependency Injection (DI) is not an optional "nice-to-have" — it’s the default and recommended way of building maintainable, testable, and loosely-coupled applications. Microsoft made DI a first-class citizen starting with .NET Core, and with every new version (especially .NET 8 and .NET 9) the built-in container gets more powerful.


>This code is a complete, up-to-date introduction to DI in .NET for developers who already know C# but want to master the current best practices.

## How to start
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
