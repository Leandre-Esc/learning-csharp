using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LCS.Infra.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    // GET
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.Users.AsNoTracking().ToListAsync(ct);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant().Trim(), ct);
    }

    // IS EXIST
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
    {
        return db.Users.AnyAsync(u => u.Email == email.ToLowerInvariant().Trim(), ct);
    }

    // CREATE
    public async Task AddSync(User user, CancellationToken ct = default)
    {
        await db.Users.AddAsync(user, ct);
    }

    // UPDATE
    public void Update(User user)
    {
        db.Users.Update(user);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return db.SaveChangesAsync(ct);
    }

    // DELETE
    public void Delete(User user)
    {
        db.Users.Remove(user);
    }
}