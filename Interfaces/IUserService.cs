namespace DependencyInjectionDemo.Interfaces;

using DependencyInjectionDemo.Models;
public interface IUserService
{
    Task<User> GetByIdAsync(Guid id);
    Task CreateAsync(User user);

}
