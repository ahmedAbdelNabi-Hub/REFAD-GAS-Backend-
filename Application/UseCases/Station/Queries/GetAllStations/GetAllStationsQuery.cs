using Application.DTOs.Station;
using Application.DTOs;
using Domain.Specification.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Station.Queries.GetAllStations
{
    public class GetAllStationsQuery : IRequest<PaginationDTO<StationDto>>
    {
        public PaginationParams PaginationParams { get; set; }
        public string VendorId { get; set; }
        public GetAllStationsQuery(PaginationParams paginationParams,string vendorId)
        {
            PaginationParams = paginationParams;
            VendorId = vendorId;
        }
    }
}
