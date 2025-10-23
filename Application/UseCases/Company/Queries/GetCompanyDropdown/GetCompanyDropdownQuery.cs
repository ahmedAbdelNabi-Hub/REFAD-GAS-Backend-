using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetCompanyDropdown
{
    public class GetCompanyDropdownQuery : IRequest<List<DropdownDto>>
    {
    }
}
