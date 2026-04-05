using System.Text;
using LCS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LCS.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUserRepository repository) : ControllerBase
{
    // GET api/v1/users
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var users = await repository.GetAllAsync(ct);
        return Ok(users);
    }

    // GET api/v1/users/{id}
    [HttpGet("{id::guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await repository.GetByIdAsync(id, ct);
        return user is null ? NotFound() : Ok(user);
    }

    // POST api/v1/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        if (await repository.ExistsByEmailAsync(request.Email, ct))
            return Conflict(new { error = "Email already in use." });

        var passwordHash = Convert.ToBase64String(
            Encoding.UTF8.GetBytes(request.Password));

        var user = Domain.Entities.User.Create(
            request.FirstName,
            request.LastName,
            request.Username,
            request.Email,
            passwordHash);

        await repository.AddSync(user, ct);
        await repository.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }
}

public record CreateUserRequest(
    string? FirstName,
    string? LastName,
    string Username,
    string Email,
    string Password
);