using Microsoft.EntityFrameworkCore;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Interfaces;
using VisitorService.Infrastructure.Context;

namespace VisitorService.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByRoleTypeAsync(RoleType roleType)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name.Value == roleType);
        }
    }
}
