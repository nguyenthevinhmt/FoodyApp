using Foody.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Foody.Application.Shared
{
    public static class CommonUtils
    {
        public static int GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            //nếu trong program dùng JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //thì các claim type sẽ không bị ghi đè tên nên phải dùng trực tiếp "sub"
            var claim = claims?.FindFirst(JwtRegisteredClaimNames.Sub) ?? claims?.FindFirst("sub");
            if (claim == null)
            {
                throw new UserFriendlyException($"Tài khoản không chứa claim \"{System.Security.Claims.ClaimTypes.NameIdentifier}\"");
            }
            int userId = int.Parse(claim.Value);
            return userId;
        }
    }
}
