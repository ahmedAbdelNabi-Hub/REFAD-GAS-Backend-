using Application.DTOs.Company;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetCompanyById
{
    public record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyDetailsDto>;

}
