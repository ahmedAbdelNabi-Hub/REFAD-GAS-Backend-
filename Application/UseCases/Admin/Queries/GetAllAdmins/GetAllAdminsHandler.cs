using Application.DTOs;
using Application.DTOs.Admins;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Admin;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsHandler : IRequestHandler<GetAllAdminsQuery, PaginationDTO<AdminDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAdminsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationDTO<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AdminWithPaginationSpecification(request.paginationParams, request.adminParams);
            var countSpec = new AdminCountSpecification(request.paginationParams, request.adminParams);

            var admins = await _unitOfWork.Repository<Domain.Entities.AdminUser>()
                .GetProjectedAsync(a => new AdminDto
                {
                    Id = a.Id,
                    Email = a.Email,
                    FullName = a.FullName,
                    Role = a.Role,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                }, spec);

            var totalCount = await _unitOfWork.Repository<Domain.Entities.AdminUser>().CountWithSpec(countSpec);

            return new PaginationDTO<AdminDto>
            {
                data = admins,
                PageIndex = request.paginationParams.PageIndex,
                PageSize = request.paginationParams.PageSize,
                Count = totalCount
            };
        }
    }
}
