using Application.DTOs.Company;
using Application.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.UpdateCompany
{
    public record UpdateCompanyCommand(Guid Id, UpdateCompanyDto CompanyDto)
       : IRequest<BaseApiResponse<int>>;
}
