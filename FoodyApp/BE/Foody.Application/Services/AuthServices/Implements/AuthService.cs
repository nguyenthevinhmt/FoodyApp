
using Foody.Application.Services.AuthServices.Dtos;
using Foody.Application.Services.AuthServices.Interfaces;
using Foody.Infrastructure.Persistence;
using Foody.Share.Constants;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Foody.Application.Services.AuthServices.Implements
{
    public class AuthService : IAuthService
    {
        private readonly FoodyAppContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(FoodyAppContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public TokenApiDto Login(UserLoginDto userInput)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == userInput.Email);
            if (user == null)
            {
                throw new UserFriendlyException($"Không tìm thấy người dùng có email {userInput.Email}");
            }
            if (!PasswordHasher.VerifyPassword(userInput.Password, user.Password))
            {
                throw new UserFriendlyException("Sai mật khẩu");
            }
            var newAccessToken = CreateJwt(userInput);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(3);
            _context.SaveChangesAsync();
            return new TokenApiDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        public void RegisterUser(CreateUserDto user)
        {
            var check = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (check != null)
            {
                throw new UserFriendlyException("Email đã tồn tại trong hệ thống");
            }
            if (user.Password.Length < 6)
            {
                throw new UserFriendlyException("Mật khẩu phải dài hơn 6 kí tự");
            }
            if (!(Regex.IsMatch(user.Password, "[a-z]") && Regex.IsMatch(user.Password, "[A-Z]") && Regex.IsMatch(user.Password, "[0-9]")))
            {
                throw new UserFriendlyException("Mật khẩu phải thuộc kiểu số và chữ");
            }
            if (!Regex.IsMatch(user.Password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                throw new UserFriendlyException("Mật khẩu phải chứa ít nhất 1 kí tự đặc biệt");

            _context.Users.Add(new Domain.Entities.User
            {
                Email = user.Email,
                Password = PasswordHasher.HashPassword(user.Password),
                UserType = user.UserType,
            });
            _context.SaveChanges();
        }
        public TokenApiDto RefreshToken(TokenApiDto input)
        {
            if (input is null)
                throw new UserFriendlyException("Invalid Client Request");
            string accessToken = input.AccessToken;
            string refreshToken = input.RefreshToken;
            var principal = GetPrincipleFromExpiredToken(accessToken);
            var email = principal.Identity.Name;

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new UserFriendlyException("Invalid Request");
            var newAccessToken = CreateJwt(new UserLoginDto { Email = user.Email, Password = user.Password });
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.SaveChangesAsync();
            return new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }
        private string CreateJwt(UserLoginDto user)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var userId = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"]);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userId.Id.ToString()),
                new Claim(CustomClaimTypes.UserType, userId.UserType.ToString())
            };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: credentials
            );
            return jwtToken.WriteToken(token);
        }
        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser = _context.Users.Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }
        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new UserFriendlyException("Hết hạn đăng nhập, vui lòng đăng nhập lại");
            return principal;
        }
    }
}
