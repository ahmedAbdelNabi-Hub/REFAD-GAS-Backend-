using Application.DTOs.Response;
using Application.Helper.Upload;
using Application.Interfaces.InternalServices;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Cars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.CreateCar
{
    public class CreateCarHandler : IRequestHandler<CreateCarCommand, BaseApiResponse<int>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public CreateCarHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CarDto;
            var existingCar = await _unitOfWork.Repository<Domain.Entities.Car>()
                .GetByIdSpecAsync(new CarsSpecification(dto.PlateNumber));

            if (existingCar != null)
                return new BaseApiResponse<int>(400, "Car with this plate number already exists.");

            string? driverImagePath = null;
            if (dto.DriverImageUrl != null)
            {
                var (response, fileName) = DocumentSettings.UploadFile(dto.DriverImageUrl, "driver");
                if (response.StatusCode != 200)
                    return new BaseApiResponse<int>(response.StatusCode, response.Message);

                driverImagePath = fileName;
            }

            var car = new Domain.Entities.Car
            {
                CompanyId = dto.CompanyId,
                PlateNumber = dto.PlateNumber,
                CarType = dto.CarType,
                FuelType = dto.FuelType,
                ControlType = dto.ControlType,
                StartDay = dto.ControlType == "Daily" ? null : dto.StartDay,
                LimitQty = dto.LimitQty,
                DriverName = dto.DriverName,
                DriverMobile = dto.DriverMobile,
                DriverPassword = _hashingService.HashPassword(dto.DriverPassword),
                DriverImageUrl = driverImagePath!,
                Status = "inactive",
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Domain.Entities.Car>().AddAsync(car);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Car created successfully");
        }
    }
}
