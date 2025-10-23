using Application.DTOs.Cars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Queries.GetCarById
{
    public class GetCarByIdQuery : IRequest<CarDto>
    {
        public Guid Id { get; set; }

        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
