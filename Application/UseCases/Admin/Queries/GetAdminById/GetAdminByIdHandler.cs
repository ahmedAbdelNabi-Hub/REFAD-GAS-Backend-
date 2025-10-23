using Application.DTOs.Admins;
using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Queries.GetAdminById
{
    public class GetAdminByIdHandler : IRequestHandler<GetAdminByIdQuery, BaseApiResponse<AdminDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAdminByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<AdminDto>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.AdminUser>();

            var admin = await repo.GetByIdAsync(request.Id);
            if (admin == null)
                return new BaseApiResponse<AdminDto>(404, "Admin not found");

            var dto = new AdminDto
            {
                Id = admin.Id,
                FullName = admin.FullName,
                Email = admin.Email,
                Role = admin.Role,
                IsActive = admin.IsActive,
                CreatedAt = admin.CreatedAt,
            };

            return new BaseApiResponse<AdminDto>(200, "Admin retrieved successfully", dto);
        }
    }

}
