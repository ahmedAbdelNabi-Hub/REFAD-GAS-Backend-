using Application.DTOs.Cars;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest<BaseApiResponse<int>>
    {
        public Guid Id { get; set; }
        public UpdateCarDto CarDto { get; set; }

        public UpdateCarCommand(Guid id, UpdateCarDto carDto)
        {
            Id = id;
            CarDto = carDto;
        }
    }
}
