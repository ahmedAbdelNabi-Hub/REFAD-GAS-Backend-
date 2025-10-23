using Application.DTOs.Cars;
using Application.DTOs.Company;
using Application.DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Car.Commands.UpdateCarfStatus
{
    public record UpdateCarStatusCommand(Guid CarId, UpdateCarStatusDto StatusDto) : IRequest<BaseApiResponse>;
    
}
