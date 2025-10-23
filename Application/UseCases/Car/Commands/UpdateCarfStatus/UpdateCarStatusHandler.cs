using Application.DTOs.Response;
using Application.UseCases.Car.Commands.UpdateCarfStatus;
using Domain.Constants;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.UpdateCarStatus
{
    public class UpdateCarStatusHandler : IRequestHandler<UpdateCarStatusCommand, BaseApiResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse> Handle(UpdateCarStatusCommand request, CancellationToken cancellationToken)
        {
            var car = await _unitOfWork.Repository<Domain.Entities.Car>().GetByIdAsync(request.CarId);

            if (car == null)
                return new BaseApiResponse(404, "Car not found.");
            var newStatus = request.StatusDto.Status?.ToLower();
            var validStatuses = new[]
            {
                Status.Pending,
                Status.Approved,
                Status.Rejected,
                Status.Suspended,
                Status.Active,
                Status.InActive
            };

            if (!validStatuses.Contains(newStatus))
                return new BaseApiResponse(400, "Invalid status value.");
            car.Status = newStatus;
            car.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse(200, $"Car status updated to '{newStatus}'.");
        }
    }
}
