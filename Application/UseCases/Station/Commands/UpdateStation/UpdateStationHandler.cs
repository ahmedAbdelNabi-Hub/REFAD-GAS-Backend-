using Application.DTOs.Response;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.UpdateStation
{
    public class UpdateStationHandler : IRequestHandler<UpdateStationCommand, BaseApiResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseApiResponse<int>> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Domain.Entities.Station>();
            var station = await repo.GetByIdAsync(request.Id);

            if (station == null)
                return new BaseApiResponse<int>(404, "Station not found.");

            var dto = request.StationDto;
            station.StationId = dto.StationId;
            station.StationNameAr = dto.StationNameAr;
            station.StationNameEn = dto.StationNameEn;
            station.LocationLat = dto.LocationLat;
            station.LocationLng = dto.LocationLng;
            station.VendorId = dto.VendorId;
            await _unitOfWork.SaveChangeAsync();

            return new BaseApiResponse<int>(200, "Station updated successfully.");
        }
    }
}
