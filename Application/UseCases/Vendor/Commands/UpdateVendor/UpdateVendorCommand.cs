using Application.DTOs.Response;
using Application.DTOs.Vendors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.UpdateVendor
{
    public class UpdateVendorCommand : IRequest<BaseApiResponse<int>>
    {
        public Guid Id { get; set; }
        public VendorDto VendorDto { get; set; }

        public UpdateVendorCommand(Guid id, VendorDto vendorDto)
        {
            Id = id;
            VendorDto = vendorDto;
        }
    }
}
