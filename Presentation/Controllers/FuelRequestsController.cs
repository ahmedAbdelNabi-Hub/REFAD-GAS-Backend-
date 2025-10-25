using Application.DTOs;
using Application.DTOs.Fuel;
using Application.UseCases.Fuel.Queries.GetAllFuelRequest;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuelRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FuelRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/fuel-requests")]
        public async Task<ActionResult<PaginationDTO<FuelRequestBasicDto>>> GetAll(
            [FromQuery(Name = "")] PaginationParams paginationParams,
            [FromQuery(Name = "")] FuelRequestParams fuelParams)
        {
            var query = new GetAllFuelRequestsQuery(paginationParams, fuelParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
