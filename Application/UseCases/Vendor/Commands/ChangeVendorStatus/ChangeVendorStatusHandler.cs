using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Vendor.Commands.ChangeVendorStatus
{
    public class ChangeVendorStatusHandler : IRequestHandler<ChangeVendorStatusCommand, BaseApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeVendorStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse> Handle(ChangeVendorStatusCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.Repository<Domain.Entities.Vendor>().GetByIdAsync(request.Id);

            if (vendor == null)
                return new BaseApiResponse(404, "Vendor not found.");

            vendor.IsActive = request.IsActive;
            vendor.UpdatedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.SaveChangeAsync();

            var statusText = request.IsActive ? "activated" : "deactivated";
            return new BaseApiResponse(200, $"Vendor successfully {statusText}.");
        }
    }
}
