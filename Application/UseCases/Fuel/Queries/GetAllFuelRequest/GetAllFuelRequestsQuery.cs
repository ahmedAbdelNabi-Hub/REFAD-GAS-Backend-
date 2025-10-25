using Application.DTOs;
using Application.DTOs.Fuel;
using Domain.Specification.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Fuel.Queries.GetAllFuelRequest
{
    public class GetAllFuelRequestsQuery : IRequest<PaginationDTO<FuelRequestBasicDto>>
    {
        public PaginationParams PaginationParams { get; set; }
        public FuelRequestParams FuelRequestParams { get; set; }

        public GetAllFuelRequestsQuery(PaginationParams paginationParams, FuelRequestParams fuelRequestParams)
        {
            PaginationParams = paginationParams;
            FuelRequestParams = fuelRequestParams;
        }
    }
}
