using Application.DTOs.Response;
using Application.DTOs.Vendors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Queries.GetVendorById
{
    public class GetVendorByIdQuery : IRequest<BaseApiResponse<VendorReadDto>>
    {
        public Guid Id { get; set; }

        public GetVendorByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
