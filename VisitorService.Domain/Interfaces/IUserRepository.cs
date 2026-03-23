using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Application.Interfaces
{
    public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> IsUserInRoleAsync(Guid id, RoleType roleName);
    Task<Name?> GetByNameAsync(Guid id);
}
}