using Application.DTOs.Response;
using Application.DTOs.Vendors;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Queries.GetVendorById
{
    public class GetVendorByIdHandler : IRequestHandler<GetVendorByIdQuery, BaseApiResponse<VendorReadDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVendorByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<VendorReadDto>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.Repository<Domain.Entities.Vendor>().GetByIdAsync(request.Id);

            if (vendor == null)
                return new BaseApiResponse<VendorReadDto>(404, "Vendor not found.");

            var dto = new VendorReadDto
            {
                Id = vendor.Id,
                VendorCode = vendor.VendorCode,
                VendorNameEn = vendor.VendorNameEn,
                VendorNameAr = vendor.VendorNameAr,
                ContactEmail = vendor.ContactEmail,
                ContactPhone = vendor.ContactPhone,
                HeadquartersAddress = vendor.HeadquartersAddress,
                Logo = vendor.LogoUrl,
                IsActive = vendor.IsActive,
                CreatedAt = vendor.CreatedAt,
                UpdatedAt = vendor.UpdatedAt,
                StationCount = vendor.Stations?.Count ?? 0
            };

            return new BaseApiResponse<VendorReadDto>(200, "Vendor retrieved successfully.", dto);
        }
    }
}
