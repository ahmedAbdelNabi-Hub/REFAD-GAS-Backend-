using Application.DTOs.Response;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, BaseApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAdminCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var adminRepo = _unitOfWork.Repository<AdminUser>();
            var admin = await adminRepo.GetByIdAsync(request.Id);

            if (admin == null)
                return new BaseApiResponse(404, "Admin not found.");

            if (!string.Equals(admin.Role, "super_admin", StringComparison.OrdinalIgnoreCase))
                return new BaseApiResponse(403, "Only super_admin can be deleted.");

            await adminRepo.DeleteAsync(admin);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse(200, "Admin deleted successfully.");
        }
    }
}
