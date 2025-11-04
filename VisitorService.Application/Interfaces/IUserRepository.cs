using System;
using System.Threading.Tasks;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.Interfaces
{
    public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
}