using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Vendors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.UpdateVendor
{
    public class UpdateVendorHandler : IRequestHandler<UpdateVendorCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVendorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<int>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var dto = request.VendorDto;

            var vendor = await _unitOfWork.Repository<Domain.Entities.Vendor>().GetByIdAsync(request.Id);
            if (vendor == null)
                return new BaseApiResponse<int>(404, "Vendor not found.");

            var existingVendor = await _unitOfWork.Repository<Domain.Entities.Vendor>()
                .GetByIdSpecAsync(new VendorSpecifications(dto.VendorCode.ToLower().Trim()));

            if (existingVendor != null && existingVendor.Id != vendor.Id)
                return new BaseApiResponse<int>(400, "Vendor code already exists.");

            vendor.VendorCode = dto.VendorCode;
            vendor.VendorNameEn = dto.VendorNameEn;
            vendor.VendorNameAr = dto.VendorNameAr;
            vendor.ContactEmail = dto.ContactEmail;
            vendor.ContactPhone = dto.ContactPhone;
            vendor.HeadquartersAddress = dto.HeadquartersAddress;
            vendor.LogoUrl = dto.Logo!;
            vendor.IsActive = dto.IsActive;
            vendor.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync();
            return new BaseApiResponse<int>(200, "Vendor updated successfully.");
        }
    }
}
