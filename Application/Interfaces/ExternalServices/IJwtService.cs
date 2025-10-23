using Application.DTOs.Auth;
using Application.DTOs.Token;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ExternalServices
{
    public interface IJwtService 
    {
        /// <summary>
        /// Generates a JWT access token and refresh token for an Admin user.
        /// </summary>
        Task<TokenResponseDto> GenerateJwtTokenForAdmin(AdminUser admin);

        /// <summary>
        /// Generates a JWT access token and refresh token for a Company account.
        /// </summary>
        Task<TokenResponseDto> GenerateJwtTokenForCompany(Company company);

        /// <summary>
        /// Generates and stores a new refresh token for a user or company.
        /// </summary>
        Task<string> GenerateRefreshToken(Guid userId);

        /// <summary>
        /// Removes token and refresh token cookies from the HTTP response.
        /// </summary>
        void RemoveTokenCookie();

    }
}
