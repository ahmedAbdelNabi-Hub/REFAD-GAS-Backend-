using Application.DTOs.Response;
using Application.DTOs.Station;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.CreateStation
{
    public class CreateStationCommand : IRequest<BaseApiResponse<int>>
    {
        public CreateStationDto StationDto { get; }

        public CreateStationCommand(CreateStationDto stationDto)
        {
            StationDto = stationDto;
        }
    }
}
