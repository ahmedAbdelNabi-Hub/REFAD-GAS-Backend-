using Application.DTOs.Company;
using Application.DTOs;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Companies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetAllCompanies
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, PaginationDTO<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCompaniesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationDTO<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var spec = new CompanyWithPaginationSpecification(request.paginationParams, request.companyParams);
            var countSpec = new CompanyCountSpecification(request.paginationParams,request.companyParams);

            var companies = await _unitOfWork.Repository<Domain.Entities.Company>()
                .GetProjectedAsync(c => new CompanyDto
                {
                    Id = c.Id,
                    CompanyNameArabic = c.CompanyNameArabic,
                    CompanyNameEnglish = c.CompanyNameEnglish,
                    ResponsiblePerson = c.ResponsiblePerson,
                    IdentityId = c.IdentityId,
                    Mobile = c.Mobile,
                    Email = c.Email,
                    Address = c.Address,
                    PumpImageRequired = c.PumpImageRequired,
                    CarImageRequired = c.CarImageRequired,
                    CarLimitType = c.CarLimitType,
                    CarLimitCount = c.CarLimitCount,
                    MonthlyCostPerCar = c.MonthlyCostPerCar,
                    Status = c.Status,
                    LogoPath = c.LogoPath,
                    DocumentsPaths = c.DocumentsPaths
                }, spec);

            var totalCount = await _unitOfWork.Repository<Domain.Entities.Company>().CountWithSpec(countSpec);
            return new PaginationDTO<CompanyDto>
            {
                data = companies,
                PageIndex = request.paginationParams.PageIndex,
                PageSize = request.paginationParams.PageSize,
                Count = totalCount
            };
        }
    }
}
