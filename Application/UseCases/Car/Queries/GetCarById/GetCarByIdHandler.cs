using Application.DTOs.Cars;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Cars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Queries.GetCarById
{
    public class GetCarByIdHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCarByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CarsSpecification(request.Id);
            var cars = await _unitOfWork.Repository<Domain.Entities.Car>()
                 .GetProjectedAsync(c => new CarDto
                 {
                     Id = c.Id,
                     CompanyId = c.CompanyId,
                     PlateNumber = c.PlateNumber,
                     CarType = c.CarType,
                     FuelType = c.FuelType,
                     Status = c.Status,
                     ControlType = c.ControlType,
                     StartDay = c.StartDay,
                     LimitQty = c.LimitQty,
                     DriverName = c.DriverName,
                     DriverMobile = c.DriverMobile,
                     DriverPassword = c.DriverPassword,
                     DriverImageUrl = c.DriverImageUrl,
                     CompanyName = c.Company.CompanyNameArabic

                 }, spec);
            return cars.FirstOrDefault();
        }
    }
}
