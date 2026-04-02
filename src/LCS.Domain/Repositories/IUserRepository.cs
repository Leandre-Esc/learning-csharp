using LCS.Domain.Entities;

namespace LCS.Domain.Repositories;

public interface IUserRepository
{
    // GET
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);

    // IS EXIST
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);

    // CREATE
    Task AddSync(User user, CancellationToken ct = default);

    // UPDATE
    void Update(User user);
    Task<int> SaveChangesAsync(CancellationToken ct = default);

    // DELETE
    void Delete(User user);
}