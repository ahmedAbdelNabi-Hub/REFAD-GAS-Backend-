using Application.DTOs.Auth;
using Application.DTOs.Response;
using Application.Interfaces.ExternalServices;
using Application.Interfaces.InternalServices;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Admin;
using Domain.Specification.Admins;
using Domain.Specification.Companies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, BaseApiResponse<AuthResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IHashingService _hashingService;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<AuthResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var email = request.LoginDto.Email.Trim().ToLower();
            var password = request.LoginDto.Password;
            var admin = await _unitOfWork.Repository<AdminUser>().GetByIdSpecAsync(new AdminSpecifications(email));
            if (admin != null)
            {
                if (!_hashingService.VerifyPassword(password, admin.PasswordHash))
                    return new BaseApiResponse<AuthResponseDto>(404, "Invalid password");
                var token = await _jwtService.GenerateJwtTokenForAdmin(admin);

                return new BaseApiResponse<AuthResponseDto>(200, "sucuess", new AuthResponseDto(
                    admin.Email,
                    admin.FullName,
                    token.Token,
                    token.RefreshToken,
                    token.ExpiresAt,
                    admin.Role
                ));
            }

            var company = await _unitOfWork.Repository<Domain.Entities.Company>().GetByIdSpecAsync(new CompanySpecifications(email));
            if (company == null)
                throw new UnauthorizedAccessException("User not found.");

            if (!_hashingService.VerifyPassword(password, company.PasswordHash))
                return new BaseApiResponse<AuthResponseDto>(404, "Invalid password");

            var companyToken = await _jwtService.GenerateJwtTokenForCompany(company);

            return new BaseApiResponse<AuthResponseDto>(200, "sucuess", new AuthResponseDto(
                     company.Email,
                     company.CompanyNameEnglish,
                     companyToken.Token,
                     companyToken.RefreshToken,
                     companyToken.ExpiresAt,
                     "company"
                     
                 ));
             }
        }
    }
