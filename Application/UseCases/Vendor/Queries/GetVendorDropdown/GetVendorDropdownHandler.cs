using Application.DTOs;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Queries.GetVendorDropdown
{
    public class GetVendorDropdownHandler : IRequestHandler<GetVendorDropdownQuery, List<DropdownDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVendorDropdownHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DropdownDto>> Handle(GetVendorDropdownQuery request, CancellationToken cancellationToken)
        {
            var vendors = await _unitOfWork.Repository<Domain.Entities.Vendor>()
                            .GetProjectedAsync(selector: v => new DropdownDto
                            {
                                Id = v.Id,
                                Name = v.VendorNameAr
                            });

            return vendors;
        }
    }
}
