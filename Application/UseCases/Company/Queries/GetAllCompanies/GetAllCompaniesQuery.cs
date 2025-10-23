using Application.DTOs.Company;
using Application.DTOs;
using Domain.Specification.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetAllCompanies
{
    public record GetAllCompaniesQuery(PaginationParams paginationParams, CompanyParams companyParams)
         : IRequest<PaginationDTO<CompanyDto>>;
}
