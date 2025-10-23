using Application.DTOs.Company;
using Application.DTOs.Response;
using Application.Helper.Upload;
using Application.Interfaces.InternalServices;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public UpdateCompanyHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CompanyDto;

            var companyRepo = _unitOfWork.Repository<Domain.Entities.Company>();
            var company = await companyRepo.GetByIdAsync(request.Id);

            if (company == null)
                return new BaseApiResponse<int>(404, "Company not found.");

            if (!string.Equals(company.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingCompany = await companyRepo
                    .GetByIdSpecAsync(new Domain.Specification.Companies.CompanySpecifications(dto.Email));

                if (existingCompany != null)
                    return new BaseApiResponse<int>(400, "Email already registered.");
            }

            if (!string.IsNullOrWhiteSpace(dto.PasswordHash))
                company.PasswordHash = _hashingService.HashPassword(dto.PasswordHash);

            // --- Update main data ---
            company.CompanyNameArabic = dto.CompanyNameArabic;
            company.CompanyNameEnglish = dto.CompanyNameEnglish;
            company.ResponsiblePerson = dto.ResponsiblePerson;
            company.IdentityId = dto.IdentityId;
            company.Mobile = dto.Mobile;
            company.Email = dto.Email;
            company.Address = dto.Address;
            company.PumpImageRequired = dto.PumpImageRequired;
            company.CarImageRequired = dto.CarImageRequired;
            company.CarLimitType = dto.CarLimitType;
            company.CarLimitCount = dto.CarLimitCount;
            company.MonthlyCostPerCar = dto.MonthlyCostPerCar;
            company.Status = dto.Status;
            company.UpdatedAt = DateTime.UtcNow;

            // --- Handle Logo Update ---
            if (dto.Logo != null)
            {
                if (!string.IsNullOrEmpty(company.LogoPath))
                {
                    var deleteResponse = DocumentSettings.DeleteFile("companies/logos", company.LogoPath);
                    if (deleteResponse.StatusCode != 200)
                        return new BaseApiResponse<int>(deleteResponse.StatusCode, deleteResponse.Message);
                }

                var (response, fileName) = DocumentSettings.UploadFile(dto.Logo, "companies/logos");
                if (response.StatusCode != 200)
                    return new BaseApiResponse<int>(response.StatusCode, response.Message);

                company.LogoPath = fileName;
            }

            // --- Handle Documents Update ---
            if (dto.Documents != null && dto.Documents.Any())
            {
                // Delete old document files if exist
                if (!string.IsNullOrEmpty(company.DocumentsPaths))
                {
                    var oldDocs = JsonSerializer.Deserialize<List<string>>(company.DocumentsPaths);
                    if (oldDocs != null)
                    {
                        foreach (var oldFile in oldDocs)
                            DocumentSettings.DeleteFile("companies/documents", oldFile);
                    }
                }

                var uploadedDocs = new List<string>();
                foreach (var file in dto.Documents)
                {
                    var (response, fileName) = DocumentSettings.UploadFile(file, "companies/documents");
                    if (response.StatusCode != 200)
                        return new BaseApiResponse<int>(response.StatusCode, response.Message);

                    if (fileName != null)
                        uploadedDocs.Add(fileName);
                }

                company.DocumentsPaths = JsonSerializer.Serialize(uploadedDocs);
            }

            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Company updated successfully.");
        }
    }
}
