using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.ChangeAdminStatus
{
    public class ChangeAdminStatusHandler : IRequestHandler<ChangeAdminStatusCommand, BaseApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeAdminStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse> Handle(ChangeAdminStatusCommand request, CancellationToken cancellationToken)
        {
            var adminRepo = _unitOfWork.Repository<Domain.Entities.AdminUser>();
            var admin = await adminRepo.GetByIdAsync(request.AdminId);

            if (admin == null)
                return new BaseApiResponse(404, "Admin not found.");

            admin.IsActive = request.IsActive;
            admin.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync();

            string statusText = admin.IsActive ? "activated" : "deactivated";
            return new BaseApiResponse(200, $"Admin successfully {statusText}.");
        }
    }
}
