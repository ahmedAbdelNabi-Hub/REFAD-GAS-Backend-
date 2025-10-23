using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Queries.GetVendorDropdown
{
    public class GetVendorDropdownQuery : IRequest<List<DropdownDto>>
    {
    }
}
