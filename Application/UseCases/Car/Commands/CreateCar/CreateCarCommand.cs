using Application.DTOs.Cars;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.CreateCar
{
    public class CreateCarCommand : IRequest<BaseApiResponse<int>>
    {
        public CreateCarDto CarDto { get; set; }

        public CreateCarCommand(CreateCarDto carDto)
        {
            CarDto = carDto;
        }
    }
}
