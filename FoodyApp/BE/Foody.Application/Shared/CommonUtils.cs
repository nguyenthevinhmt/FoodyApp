using Foody.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Foody.Application.Shared
{
    public static class CommonUtils
    {
        public static int GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var claim = claims?.FindFirst(ClaimTypes.NameIdentifier) ?? claims?.FindFirst("sub");
            if (claim == null)
            {
                throw new UserFriendlyException($"Tài khoản không chứa claim \"{ClaimTypes.NameIdentifier}\"");
            }
            int userId = int.Parse(claim.Value);
            return userId;
        }
    }
}
