using Application.DTOs.Response;
using Application.DTOs.Station;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.UpdateStation
{
    public class UpdateStationCommand : IRequest<BaseApiResponse<int>>
    {
        public Guid Id { get; }
        public UpdateStationDto StationDto { get; }

        public UpdateStationCommand(Guid id, UpdateStationDto dto)
        {
            Id = id;
            StationDto = dto;
        }
    }
}
