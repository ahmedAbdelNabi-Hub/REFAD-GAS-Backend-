using Application.DTOs;
using Application.DTOs.Vendors;
using Domain.Specification.Params;
using MediatR;

namespace Application.UseCases.Vendor.Queries.GetAllVendors
{
    public class GetAllVendorsQuery : IRequest<PaginationDTO<VendorReadDto>>
    {
        public PaginationParams paginationParams { get; }
        public VendorParams vendorParams { get; }

        public GetAllVendorsQuery(PaginationParams paginationParams, VendorParams vendorParams)
        {
            this.paginationParams = paginationParams;
            this.vendorParams = vendorParams;
        }
    }
}
