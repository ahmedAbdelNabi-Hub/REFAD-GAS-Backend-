using Application.DTOs.Company;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<BaseApiResponse<int>> 
    {
        public CreateCompanyDto CompanyDto { get; set; }

        public CreateCompanyCommand(CreateCompanyDto companyDto)
        {
            CompanyDto = companyDto;
        }
    }
}
