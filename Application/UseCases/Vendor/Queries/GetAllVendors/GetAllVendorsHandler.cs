using Application.DTOs;
using Application.DTOs.Vendors;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Vendor;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Queries.GetAllVendors
{
    public class GetAllVendorsHandler : IRequestHandler<GetAllVendorsQuery, PaginationDTO<VendorReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVendorsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationDTO<VendorReadDto>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            var spec = new VendorWithPaginationSpecification(request.paginationParams, request.vendorParams);
            var countSpec = new VendorCountSpecification(request.paginationParams, request.vendorParams);

            var vendors = await _unitOfWork.Repository<Domain.Entities.Vendor>()
                .GetProjectedAsync(v => new VendorReadDto
                {
                    Id = v.Id,
                    VendorCode = v.VendorCode,
                    VendorNameEn = v.VendorNameEn,
                    VendorNameAr = v.VendorNameAr,
                    ContactEmail = v.ContactEmail,
                    ContactPhone = v.ContactPhone,
                    HeadquartersAddress = v.HeadquartersAddress,
                    Logo = v.LogoUrl,
                    IsActive = v.IsActive,
                    StationCount = v.Stations.Count, 
                    CreatedAt = v.CreatedAt,
                    UpdatedAt = v.UpdatedAt
                }, spec);

            var totalCount = await _unitOfWork.Repository<Domain.Entities.Vendor>().CountWithSpec(countSpec);

            return new PaginationDTO<VendorReadDto>
            {
                data = vendors,
                PageIndex = request.paginationParams.PageIndex,
                PageSize = request.paginationParams.PageSize,
                Count = totalCount
            };
        }
    }
}
