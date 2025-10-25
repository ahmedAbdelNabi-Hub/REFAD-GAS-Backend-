using Application.DTOs.Station;
using Application.DTOs;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Stations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Queries.GetAllStations
{
    public class GetAllStationsHandler : IRequestHandler<GetAllStationsQuery, PaginationDTO<StationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStationsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationDTO<StationDto>> Handle(GetAllStationsQuery request, CancellationToken cancellationToken)
        {

            var spec = new StationWithPaginationSpecification(request.PaginationParams, request.VendorId);
            var countSpec = new StationCountSpecification(request.PaginationParams,  request.VendorId);

            var stations = await _unitOfWork.Repository<Domain.Entities.Station>()
                .GetProjectedAsync(s => new StationDto
                {
                    Id = s.Id,
                    StationId = s.StationId,
                    StationNameAr = s.StationNameAr,
                    StationNameEn = s.StationNameEn,
                    LocationLat = s.LocationLat,
                    LocationLng = s.LocationLng,
                    VendorNameAr = s.Vendor.VendorNameAr,
                    VendorNameEn = s.Vendor.VendorNameEn,
                    FuelRequestCount = s.FuelRequests.Count,
                    VendorId = s.Vendor.Id,
                }, spec);

            var totalCount = await _unitOfWork.Repository<Domain.Entities.Station>().CountWithSpec(countSpec);
            return new PaginationDTO<StationDto>
            {
                data = stations,
                PageIndex = request.PaginationParams.PageIndex,
                PageSize = request.PaginationParams.PageSize,
                Count = totalCount
            };
        }
    }
}
