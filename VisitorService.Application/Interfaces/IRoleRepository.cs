using System.Threading.Tasks;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;

namespace VisitorService.Application.Interfaces
{
    public interface IRoleRepository
{
    Task<Role?> GetByRoleTypeAsync(RoleType roleType);
}
}