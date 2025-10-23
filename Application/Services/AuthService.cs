//using Application.DTOs.Auth;
//using Application.Interfaces.InternalServices;
//using Application.Interfaces.ExternalServices;
//using Domain.Interfaces.UnitOfWork;
//using Application.DTOs.Response;

//using Domain.Specification;
//using Microsoft.EntityFrameworkCore;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using Domain.Entities.Identity;
//using Microsoft.AspNetCore.Identity;
//using Application.DTOs.Token;
//using Microsoft.AspNetCore.Http;
//using Domain.Entities;
//namespace Application.Services
//{
//    internal class AuthService : IAuthService
//    {
//        private readonly IJwtService _jwtService;
//        private IUnitOfWork _unitOfWork;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        public AuthService(IJwtService jwtService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager , IHttpContextAccessor httpContextAccessor)
//        {
//            _jwtService = jwtService;
//            _unitOfWork = unitOfWork;
//            _userManager = userManager;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public Task<BaseApiResponse> GetCurrentUserAsync(string guid)
//        {
//            throw new NotImplementedException();
//        }


//        public async Task<BaseApiResponse> RefreshTokenAsync(string token)
//        {
//            // 1. Check if refresh token exists
//            var refreshToken = await _unitOfWork.Repository<RefreshToken>()
//                .GetByIdSpecAsync(new RefreshTokenSpecifications(token));

//            if (refreshToken == null)
//            {
//                return new BaseApiResponse<bool>
//                {
//                    StatusCode = 401,
//                    Message = "Invalid refresh token.",
//                    Data = false
//                };
//            }

//            // 2. Check if expired
//            if (refreshToken.IsExpired)
//            {
//                return new BaseApiResponse<bool>
//                {
//                    StatusCode = 401,
//                    Message = "Refresh token expired.",
//                    Data = false
//                };
//            }

//            // 3. Get user
//            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
//            if (user == null)
//            {
//                return new BaseApiResponse<bool>
//                {
//                    StatusCode = 404,
//                    Message = "User not found.",
//                    Data = false
//                };
//            }

//            // 5. Generate new JWT (without generating new refresh token unless you want rotation)
//            var jwtResponse = await _jwtService.GenerateJwtToken(user);

//            return new BaseApiResponse<TokenResponseDto>
//            {
//                StatusCode = 200,
//                Message = "Token refreshed successfully.",
//                Data = new TokenResponseDto
//                {
//                    Token = jwtResponse.Token,
//                    ExpiresAt = jwtResponse.ExpiresAt,
//                    Roles = jwtResponse.Roles
//                }
//            };
//        }

//        public async Task<BaseApiResponse> LogoutAsync()
//        {

//            _jwtService.RemoveTokenCookie();
//            return await Task.FromResult(new BaseApiResponse
//            {
//                StatusCode = 200,
//                Message = "Logged out successfully"
//            });
//        }

//    }
//}
