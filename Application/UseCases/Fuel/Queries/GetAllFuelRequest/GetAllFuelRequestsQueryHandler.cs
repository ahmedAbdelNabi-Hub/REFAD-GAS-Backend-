using Application.DTOs.Company;
using Application.DTOs;
using Application.DTOs.Fuel;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Companies;
using Domain.Specification.FuelRequests;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Fuel.Queries.GetAllFuelRequest
{
    public class GetAllFuelRequestsQueryHandler : IRequestHandler<GetAllFuelRequestsQuery, PaginationDTO<FuelRequestBasicDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFuelRequestsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationDTO<FuelRequestBasicDto>> Handle(GetAllFuelRequestsQuery request, CancellationToken cancellationToken)
        {
            var spec = new FuelRequestWithPaginationsSpecification(request.PaginationParams, request.FuelRequestParams);
            var countSpec = new FuelRequestWithCountSpecification(request.PaginationParams, request.FuelRequestParams);

            var fuelRequests = await _unitOfWork.Repository<FuelRequest>()
                .GetProjectedAsync(fr => new FuelRequestBasicDto
                {
                    Id = fr.Id,
                    CompanyName = fr.Company.CompanyNameArabic ?? fr.Company.CompanyNameEnglish,
                    CarPlateNumber = fr.Car.PlateNumber,
                    StationName = fr.Station.StationNameAr,
                    CarName = fr.Car.CarType,
                    Qty = fr.Qty,
                    Amount = fr.Amount,
                    Status = fr.Status,
                    FuelType = fr.Car.FuelType,
                    RequestDate = fr.RequestDateTime,
                   
                }, spec);
            var totalCount = await _unitOfWork.Repository<Domain.Entities.FuelRequest>().CountWithSpec(countSpec);
            return new PaginationDTO<FuelRequestBasicDto>
            {
                data = fuelRequests,
                PageIndex = request.PaginationParams.PageIndex,
                PageSize = request.PaginationParams.PageSize,
                Count = totalCount
            };
           
        }
    }
}
