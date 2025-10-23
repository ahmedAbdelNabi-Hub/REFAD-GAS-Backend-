using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Commands.DeleteStation
{
    public class DeleteStationCommand : IRequest<BaseApiResponse<int>>
    {
        public Guid Id { get; }
        public DeleteStationCommand(Guid id)
        {
            Id = id;
        }
    }
}
