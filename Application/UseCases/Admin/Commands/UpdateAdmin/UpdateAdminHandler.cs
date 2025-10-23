using Application.DTOs.Response;
using Application.Interfaces.InternalServices;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.UpdateAdmin
{
    public class UpdateAdminHandler : IRequestHandler<UpdateAdminCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public UpdateAdminHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AdminDto;

            var adminRepo = _unitOfWork.Repository<AdminUser>();
            var admin = await adminRepo.GetByIdAsync(request.Id);

            if (admin == null)
                return new BaseApiResponse<int>(404, "Admin not found.");

            if (!string.Equals(admin.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingAdmin = await adminRepo
                    .GetByIdSpecAsync(new Domain.Specification.Admins.AdminSpecifications(dto.Email));

                if (existingAdmin != null)
                    return new BaseApiResponse<int>(400, "Email already registered.");
            }

            if (!string.IsNullOrWhiteSpace(dto.PasswordHash))
                admin.PasswordHash = _hashingService.HashPassword(dto.PasswordHash);

            admin.FullName = dto.FullName;
            admin.Email = dto.Email;
            admin.Role = string.IsNullOrWhiteSpace(dto.Role) ? admin.Role : dto.Role.ToLower();
            admin.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Admin updated successfully.");
        }
    }
}
