using System.Threading.Tasks;

namespace VisitorService.Application.Interfaces
{
    public interface IAuthService
{
    Task<string> GenerateTokenAsync(Guid userId, string email, IEnumerable<string> roles);
}
}