using Application.DTOs.Company;
using Application.DTOs;
using Application.UseCases.Company.Queries.GetAllCompanies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Cars;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Companies;
using Domain.Specification.Cars;

namespace Application.UseCases.Car.Queries.GetAllCars
{
    public class GetAllCarsHandler : IRequestHandler<GetAllCarsQuery, PaginationDTO<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCarsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginationDTO<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var spec = new CarWithPaginationSpecification(request.paginationParams, request.carParams);
            var countSpec = new CarCountSpecification(request.paginationParams, request.carParams);

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
                      UsedQty =c.UsedQty,
                      DriverName = c.DriverName,
                      DriverMobile = c.DriverMobile,
                      DriverPassword = c.DriverPassword,
                      DriverImageUrl = c.DriverImageUrl,
                      CompanyName = c.Company.CompanyNameArabic
                      
                  }, spec);

            var totalCount = await _unitOfWork.Repository<Domain.Entities.Car>().CountWithSpec(countSpec);
            return new PaginationDTO<CarDto>
            {
                data = cars,
                PageIndex = request.paginationParams.PageIndex,
                PageSize = request.paginationParams.PageSize,
                Count = totalCount
            };
        }
    }
}
