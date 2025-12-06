using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;

namespace VisitorService.Domain.Interfaces
{
    public interface IRoleRepository
{
    Task<Role?> GetByRoleTypeAsync(RoleType roleType);
}
}