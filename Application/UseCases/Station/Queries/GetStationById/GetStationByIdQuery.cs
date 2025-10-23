using Application.DTOs.Station;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Queries.GetStationById
{
    public class GetStationByIdQuery : IRequest<StationDto>
    {
        public Guid Id { get; }

        public GetStationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
