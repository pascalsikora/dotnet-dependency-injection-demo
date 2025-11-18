namespace DependencyInjectionDemo.Controllers;

using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Interfaces;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
        => this.userService = userService ?? throw new ArgumentNullException(nameof(userService));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> Get(Guid id)
        => await this.userService.GetByIdAsync(id) is User user
            ? this.Ok(user)
            : this.NotFound();

    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] User user)
    {
        var created = this.userService.CreateAsync(user);
        return this.CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

}