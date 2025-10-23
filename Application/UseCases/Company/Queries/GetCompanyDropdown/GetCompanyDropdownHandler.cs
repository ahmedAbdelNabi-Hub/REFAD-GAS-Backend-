using Application.DTOs;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetCompanyDropdown
{
    public class GetCompanyDropdownHandler : IRequestHandler<GetCompanyDropdownQuery, List<DropdownDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyDropdownHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DropdownDto>> Handle(GetCompanyDropdownQuery request, CancellationToken cancellationToken)
        {
            var companies = await _unitOfWork.Repository<Domain.Entities.Company>()
                           .GetProjectedAsync(selector: c=>new DropdownDto
                           {
                                Id = c.Id,
                                Name = c.CompanyNameArabic
                           });

            return companies;
        }
    }
}
