using Application.DTOs.Company;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Queries.GetCompanyById
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyDetailsDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Domain.Entities.Company>().GetByIdAsync(request.CompanyId);
            if (company == null)
                return null;

            return new CompanyDetailsDto
            {
                Id = company.Id,
                CompanyNameArabic = company.CompanyNameArabic,
                CompanyNameEnglish = company.CompanyNameEnglish,
                ResponsiblePerson = company.ResponsiblePerson,
                IdentityId = company.IdentityId,
                Mobile = company.Mobile,
                Email = company.Email,
                Address = company.Address,
                PumpImageRequired = company.PumpImageRequired,
                CarImageRequired = company.CarImageRequired,
                CarLimitType = company.CarLimitType,
                CarLimitCount = company.CarLimitCount,
                MonthlyCostPerCar = company.MonthlyCostPerCar,
                Status = company.Status,
                LogoPath = company.LogoPath,
                DocumentsPaths = company.DocumentsPaths,
                CreatedAt = company.CreatedAt

            };
        }
    }

}
