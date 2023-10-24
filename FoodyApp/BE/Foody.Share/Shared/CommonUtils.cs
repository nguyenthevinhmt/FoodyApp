using Foody.Share.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Foody.Share.Shared
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
        public static string GetEmail(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var claim = claims?.FindFirst(JwtRegisteredClaimNames.Email) ?? claims?.FindFirst("name");
            if (claim == null)
            {
                throw new UserFriendlyException($"Tài khoản không chứa claim \"{ClaimTypes.NameIdentifier}\"");
            }
            string email = claim.Value;
            return email;
        }
        public static string GetIpAddress(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
}
