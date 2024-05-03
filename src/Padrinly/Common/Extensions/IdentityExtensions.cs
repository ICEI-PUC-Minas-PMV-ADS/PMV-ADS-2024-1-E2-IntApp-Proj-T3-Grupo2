using System.Security.Claims;

namespace Padrinly.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
