using Application.DTOs.Company;
using Application.DTOs.Response;
using Application.Helper.Upload;
using Application.Interfaces.InternalServices;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public CreateCompanyHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CompanyDto;
           
            var existingCompany = await _unitOfWork.Repository<Domain.Entities.Company>()
                .GetByIdSpecAsync(new Domain.Specification.Companies.CompanySpecifications(dto.Email));

            if (existingCompany != null)
                return new BaseApiResponse<int>(400, "Email already registered.");

            var hashedPassword = _hashingService.HashPassword(dto.PasswordHash);
            string? logoFileName = null;
            if (dto.Logo != null)
            {
                var (response, fileName) = DocumentSettings.UploadFile(dto.Logo, "companies/logos");
                if (response.StatusCode != 200)
                    return new BaseApiResponse<int>(response.StatusCode, response.Message);

                logoFileName = fileName;
            }

            string? documentsFileNames = null;
            if (dto.Documents != null && dto.Documents.Any())
            {
                var uploadedImages = new List<string>();
                foreach (var image in dto.Documents)
                {
                    var (response, fileName) = DocumentSettings.UploadFile(image, "companies/documents"); 
                    if (response.StatusCode != 200)
                        return new BaseApiResponse<int>(response.StatusCode, response.Message);

                    if (fileName != null)
                        uploadedImages.Add(fileName);
                }

                documentsFileNames = System.Text.Json.JsonSerializer.Serialize(uploadedImages);
            }

            var company = new Domain.Entities.Company
            {
                CompanyNameArabic = dto.CompanyNameArabic,
                CompanyNameEnglish = dto.CompanyNameEnglish,
                ResponsiblePerson = dto.ResponsiblePerson,
                IdentityId = dto.IdentityId,
                Mobile = dto.Mobile,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Address = dto.Address,
                PumpImageRequired = dto.PumpImageRequired,
                CarImageRequired = dto.CarImageRequired,
                CarLimitType = dto.CarLimitType,
                CarLimitCount = dto.CarLimitCount,
                MonthlyCostPerCar = dto.MonthlyCostPerCar,
                Status = dto.Status,
                LogoPath = logoFileName,
                DocumentsPaths = documentsFileNames,
                CreatedAt = DateTime.UtcNow
            };


            await _unitOfWork.Repository<Domain.Entities.Company>().AddAsync(company);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Company created successfully");
        }
    }
}
