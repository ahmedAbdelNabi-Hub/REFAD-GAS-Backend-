using Application.DTOs.Response;
using Application.Helper.Upload;
using Application.Interfaces.InternalServices;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.UpdateCar
{
    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;

        public UpdateCarHandler(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<BaseApiResponse<int>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CarDto;

            var carRepo = _unitOfWork.Repository<Domain.Entities.Car>();
            var car = await carRepo.GetByIdAsync(request.Id);

            if (car == null)
                return new BaseApiResponse<int>(404, "Car not found.");

            if (!string.Equals(car.PlateNumber, dto.PlateNumber, StringComparison.OrdinalIgnoreCase))
            {
                var existingCar = await carRepo
                    .GetByIdSpecAsync(new Domain.Specification.Cars.CarsSpecification(dto.PlateNumber));

                if (existingCar != null && existingCar.Id.ToString() != car.Id.ToString())
                    return new BaseApiResponse<int>(400, "Plate number already registered.");
            }


            if (!string.IsNullOrWhiteSpace(dto.DriverPassword))
                car.DriverPassword = _hashingService.HashPassword(dto.DriverPassword);

            car.CompanyId = dto.CompanyId;
            car.PlateNumber = dto.PlateNumber;
            car.CarType = dto.CarType;
            car.FuelType = dto.FuelType;
            car.ControlType = dto.ControlType;
            car.StartDay = dto.ControlType == "Daily" ? null : dto.StartDay;
            car.LimitQty = dto.LimitQty;
            car.DriverName = dto.DriverName;
            car.DriverMobile = dto.DriverMobile;
            car.UpdatedAt = DateTime.UtcNow;

            if (dto.DriverImageFile != null)
            {
                if (!string.IsNullOrEmpty(car.DriverImageUrl))
                {
                    var deleteResponse = DocumentSettings.DeleteFile("driver", car.DriverImageUrl);
                    if (deleteResponse.StatusCode != 200)
                        return new BaseApiResponse<int>(deleteResponse.StatusCode, deleteResponse.Message);
                }

                var (uploadResponse, fileName) = DocumentSettings.UploadFile(dto.DriverImageFile, "driver");
                if (uploadResponse.StatusCode != 200)
                    return new BaseApiResponse<int>(uploadResponse.StatusCode, uploadResponse.Message);

                car.DriverImageUrl = fileName;
            }

            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Car updated successfully.");
        }
    }
}
