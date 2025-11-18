using DependencyInjectionDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionDemo.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
}