using System.Security.Claims;

namespace VisitorService.Interfaces.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("nameid");

            if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                return Guid.Empty;

            return Guid.TryParse(claim.Value, out var userId) ? userId : Guid.Empty;
        }
    }
}