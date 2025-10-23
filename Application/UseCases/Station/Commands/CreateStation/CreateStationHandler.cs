using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.CreateStation
{
    public class CreateStationHandler : IRequestHandler<CreateStationCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<int>> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {
            var dto = request.StationDto;
            var station = new Domain.Entities.Station
            {
                StationId = dto.StationId,
                VendorId = dto.VendorId,
                LocationLat = dto.LocationLat,
                LocationLng = dto.LocationLng,
                StationNameAr = dto.StationNameAr,
                StationNameEn = dto.StationNameEn
            };

            await _unitOfWork.Repository<Domain.Entities.Station>().AddAsync(station);
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Station created successfully.");
        }
    }
}
