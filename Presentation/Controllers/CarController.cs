using Application.DTOs.Cars;
using Application.DTOs.Company;
using Application.DTOs.Response;
using Application.UseCases.Car.Commands.CreateCar;
using Application.UseCases.Car.Commands.UpdateCar;
using Application.UseCases.Car.Commands.UpdateCarfStatus;
using Application.UseCases.Car.Queries;
using Application.UseCases.Car.Queries.GetAllCars;
using Application.UseCases.Car.Queries.GetCarById;
using Application.UseCases.Company.Commands.CreateCompany;
using Application.UseCases.Company.Commands.UpdateCompany;
using Application.UseCases.Company.Commands.UpdateCompanyStatus;
using Application.UseCases.Company.Queries.GetAllCompanies;
using Application.UseCases.Company.Queries.GetCompanyById;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/cars")]
        public async Task<IActionResult> GetAllCars([FromQuery(Name = "")] PaginationParams paginationParams, [FromQuery(Name = "")] CarParams carParams)
        {
            var query = new GetAllCarsQuery(paginationParams, carParams);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/api/cars/{id}")]
        public async Task<IActionResult> GetCarsById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var carsId))
                return BadRequest(new { Message = "Invalid cars ID format." });

            var result = await _mediator.Send(new GetCarByIdQuery(carsId));
            if (result == null)
                return NotFound(new { Message = "car not found." });
            return Ok(result);
        }

        [HttpPost("/api/cars")]
        public async Task<IActionResult> CreateCar([FromForm] CreateCarDto carDto)
        {
            if (carDto == null)
                return BadRequest(new BaseApiResponse<int>(400, "Invalid company data."));
            var command = new CreateCarCommand(carDto);
            var result = await _mediator.Send(command);
            if (result.StatusCode != 200)
                return StatusCode(result.StatusCode, result);
            return Ok(result);
        }
        
        
        [HttpPut("api/cars/{id}/status")]
        public async Task<IActionResult> UpdateCarStatus([FromRoute] string id, [FromBody] UpdateCarStatusDto dto)
        {
            if (!Guid.TryParse(id, out var carId))
                return BadRequest(new { Message = "Invalid company ID format." });

            var result = await _mediator.Send(new UpdateCarStatusCommand(carId, dto));
            return StatusCode(result.StatusCode, result);
        }


        [HttpPut("/api/cars/{id}")]
        public async Task<IActionResult> UpdateCars([FromRoute] string id, [FromForm] UpdateCarDto dto)
        {
            if (!Guid.TryParse(id, out var carId))
                return BadRequest(new { message = "Invalid car ID format." });

            var result = await _mediator.Send(new UpdateCarCommand(carId, dto));
            return StatusCode(result.StatusCode, result);
        }


    }

}
