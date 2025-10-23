using Application.DTOs.Station;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Stations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Queries.GetStationById
{
    public class GetStationByIdHandler : IRequestHandler<GetStationByIdQuery, StationDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStationByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StationDto> Handle(GetStationByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new StationSpecifications(request.Id.ToString());
            var station = await _unitOfWork.Repository<Domain.Entities.Station>()
                .GetProjectedAsync(s => new StationDto
                {
                    Id = s.Id,
                    StationId = s.StationId,
                    StationNameAr = s.StationNameAr,
                    StationNameEn = s.StationNameEn,
                    LocationLat = s.LocationLat,
                    LocationLng = s.LocationLng,
                    VendorId = s.VendorId,

                }, spec);
            return station.FirstOrDefault();
        }
    }
}
