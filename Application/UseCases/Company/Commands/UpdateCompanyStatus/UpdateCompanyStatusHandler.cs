using Application.DTOs.Response;
using Domain.Constants;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Company.Commands.UpdateCompanyStatus
{
    public class UpdateCompanyStatusHandler : IRequestHandler<UpdateCompanyStatusCommand, BaseApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse> Handle(UpdateCompanyStatusCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Domain.Entities.Company>().GetByIdAsync(request.CompanyId);

            if (company == null)
                return new BaseApiResponse(404, "Company not found.");

            var newStatus = request.StatusDto.Status?.ToLower();

            var validStatuses = new[]
            {
                Status.Pending,
                Status.Approved,
                Status.Rejected,
                Status.Suspended,
                Status.Active,
            };

            if (!validStatuses.Contains(newStatus))
                return new BaseApiResponse(400, "Invalid status value.");

            company.Status = newStatus;
            company.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangeAsync();
            return new BaseApiResponse(200, $"Company status updated to '{newStatus}'.");
        }
    }
}
