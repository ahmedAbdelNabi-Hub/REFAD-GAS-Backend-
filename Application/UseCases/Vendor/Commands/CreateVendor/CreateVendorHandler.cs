using Application.DTOs.Response;
using Application.Helper.Upload;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Vendors;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.CreateVendor
{
    public class CreateVendorHandler : IRequestHandler<CreateVendorCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateVendorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<int>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var dto = request.VendorDto;
            var existingVendor = await _unitOfWork.Repository<Domain.Entities.Vendor>()
                .GetByIdSpecAsync(new VendorSpecifications(dto.VendorCode));

            if (existingVendor != null)
                return new BaseApiResponse<int>(400, "Vendor code already exists.");
      
            var vendor = new Domain.Entities.Vendor
            {
                VendorCode = dto.VendorCode,
                VendorNameEn = dto.VendorNameEn,
                VendorNameAr = dto.VendorNameAr,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                HeadquartersAddress = dto.HeadquartersAddress,
                LogoUrl = dto.Logo!,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Domain.Entities.Vendor>().AddAsync(vendor);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Vendor created successfully.");
        }
    }
}
