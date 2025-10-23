using Application.DTOs.Token;
using Application.Interfaces.ExternalServices;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenConfig _tokenConfig;
        private readonly IUnitOfWork _unitOfWork;

        public JwtService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IOptions<TokenConfig> tokenOptions)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _tokenConfig = tokenOptions?.Value ?? throw new ArgumentNullException(nameof(tokenOptions));
        }

        // ============================
        // JWT FOR ADMIN USER
        // ============================
        public async Task<TokenResponseDto> GenerateJwtTokenForAdmin(AdminUser admin)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Name, admin.FullName),
                new Claim(ClaimTypes.Role, admin.Role)
            };

            return await GenerateJwtTokenInternal(claims, admin.Id);
        }

        // ============================
        // JWT FOR COMPANY
        // ============================
        public async Task<TokenResponseDto> GenerateJwtTokenForCompany(Company company)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, company.Id.ToString()),
                new Claim(ClaimTypes.Email, company.Email),
                new Claim(ClaimTypes.Name, company.CompanyNameEnglish),
                new Claim(ClaimTypes.Role, "company")
            };

            return await GenerateJwtTokenInternal(claims, company.Id);
        }

        // ============================
        // REFRESH TOKEN GENERATION
        // ============================
        public async Task<string> GenerateRefreshToken(Guid userId)
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = userId
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            await _unitOfWork.SaveChangeAsync();

            SetRefreshTokenCookie(refreshToken.Token, refreshToken.Expires);
            return refreshToken.Token;
        }

        // ============================
        // PRIVATE HELPER METHODS
        // ============================
        private async Task<TokenResponseDto> GenerateJwtTokenInternal(IEnumerable<Claim> claims, Guid userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_tokenConfig.Expiration);

            var token = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            SetTokenCookie(jwtToken, expiration);

            var refreshToken = await GenerateRefreshToken(userId);

            return new TokenResponseDto
            {
                Token = jwtToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiration
            };
        }

        private void SetTokenCookie(string token, DateTime expires)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expires,
                Path = "/"
            });
        }

        private void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expires,
                Path = "/"
            });
        }

        public void RemoveTokenCookie()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("token");
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
        }
    }
}
