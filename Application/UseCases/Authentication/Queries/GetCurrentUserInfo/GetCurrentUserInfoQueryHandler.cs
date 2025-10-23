using Application.DTOs.Auth;
using Application.DTOs.Response;
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

namespace Application.UseCases.Authentication.Queries.GetCurrentUserInfo
{
    public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetCurrentUserInfoQuery, BaseApiResponse<UserInfoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCurrentUserInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<UserInfoDto>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLower();
            var admin = await _unitOfWork.Repository<AdminUser>().GetByIdSpecAsync(new AdminSpecifications(email));
            if (admin != null)
            {
                var dto = new UserInfoDto
                {
                    Id = admin.Id,
                    FullName = admin.FullName,
                    Email = admin.Email,
                    Role = admin.Role,
                    UserType = "Admin"
                };

                return new BaseApiResponse<UserInfoDto>(200, "Admin user info retrieved successfully.", dto);
            }

            var company = await _unitOfWork.Repository< Domain.Entities.Company>().GetByIdSpecAsync(new CompanySpecifications(email));
            if (company != null)
            {
                var dto = new UserInfoDto
                {
                    Id = company.Id,
                    FullName = company.CompanyNameEnglish,
                    Email = company.Email,
                    Role = "Company",
                    UserType = "Company"
                };

                return new BaseApiResponse<UserInfoDto>(200, "Company user info retrieved successfully.", dto);
            }

            return new BaseApiResponse<UserInfoDto>(404, "User not found.");
        }
    }
}
