using Application.DTOs.Response;
using Application.Interfaces.InternalServices;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Admin.Commands.CreateAdmin
{
    public class CreateAdminHandler : IRequestHandler<CreateAdminCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public CreateAdminHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var dto = request;

            var existingAdmin = await _unitOfWork.Repository<AdminUser>()
                .GetByIdSpecAsync(new Domain.Specification.Admins.AdminSpecifications(dto.Email));

            if (existingAdmin != null)
                return new BaseApiResponse<int>(400, "Email already registered.");

            var hashedPassword = _hashingService.HashPassword(dto.PasswordHash);

            var admin = new AdminUser
            {
                Email = dto.Email,
                PasswordHash = hashedPassword,
                FullName = dto.FullName,
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "super_admin" : dto.Role.ToLower(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<AdminUser>().AddAsync(admin);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Admin created successfully.");
        }
    }
}
