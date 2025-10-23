using Application.DTOs.Cars;
using Application.DTOs;
using Domain.Specification.Params;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Queries.GetAllCars
{
    public record GetAllCarsQuery(PaginationParams paginationParams, CarParams carParams)
             : IRequest<PaginationDTO<CarDto>>;
}
