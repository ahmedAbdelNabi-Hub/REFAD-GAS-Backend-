using Application.DTOs.Company;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.UpdateCompanyStatus
{
    public record UpdateCompanyStatusCommand(Guid CompanyId, UpdateCompanyStatusDto StatusDto) : IRequest<BaseApiResponse>;

}
