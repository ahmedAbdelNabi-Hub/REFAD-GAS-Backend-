using Application.DTOs.Response;
using Application.DTOs.Vendors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.CreateVendor
{
    public class CreateVendorCommand : IRequest<BaseApiResponse<int>>
    {
        public VendorDto VendorDto { get; set; }

        public CreateVendorCommand(VendorDto vendorDto)
        {
            VendorDto = vendorDto;
        }
    }
}
