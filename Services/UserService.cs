namespace DependencyInjectionDemo.Services;

using DependencyInjectionDemo.Data;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ApplicationDbContext databaseContext;
    private readonly ILogger<UserService> logger;

    public UserService(ApplicationDbContext databaseContext, ILogger<UserService> logger)
    {
        this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        this.logger.LogInformation("Fetching user {UserId}", id);
        return await this.databaseContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateAsync(User user)
    {
        var toAdd = user with { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
        this.databaseContext.Users.Add(toAdd);
        await this.databaseContext.SaveChangesAsync();
        this.logger.LogInformation("User created: {UserId}", toAdd.Id);
        return toAdd;
    }

    Task IUserService.CreateAsync(User user)
    {
        return CreateAsync(user);
    }
}