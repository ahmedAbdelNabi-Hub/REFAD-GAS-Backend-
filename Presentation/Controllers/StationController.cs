using Application.DTOs.Response;
using Application.DTOs.Station;
using Application.UseCases.Station.Commands.CreateStation;
using Application.UseCases.Station.Commands.UpdateStation;
using Application.UseCases.Station.Commands.DeleteStation;
using Application.UseCases.Station.Queries.GetAllStations;
using Application.UseCases.Station.Queries.GetStationById;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/stations/{id}")]
        public async Task<IActionResult> GetStationById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var stationId))
                return BadRequest(new { Message = "Invalid station ID format." });

            var result = await _mediator.Send(new GetStationByIdQuery(stationId));

            if (result == null)
                return NotFound(new { Message = "Station not found." });

            return Ok(result);
        }

     
        [HttpGet("/api/stations")]
        public async Task<IActionResult> GetAllStations(
            [FromQuery(Name = "")] PaginationParams paginationParams, [FromQuery] string? VendorId ) 
        {

            var query = new GetAllStationsQuery(paginationParams, VendorId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

     
        [HttpPost("/api/stations")]
        public async Task<IActionResult> CreateStation([FromBody] CreateStationDto dto)
        {
            if (dto == null)
                return BadRequest(new BaseApiResponse<int>(400, "Invalid station data."));

            var result = await _mediator.Send(new CreateStationCommand(dto));

            if (result.StatusCode != 200)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPut("/api/stations/{id}")]
        public async Task<IActionResult> UpdateStation([FromRoute] string id, [FromBody] UpdateStationDto dto)
        {
            if (!Guid.TryParse(id, out var stationId))
                return BadRequest(new { Message = "Invalid station ID format." });

            var result = await _mediator.Send(new UpdateStationCommand(stationId, dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/stations/{id}")]
        public async Task<IActionResult> DeleteStation([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var stationId))
                return BadRequest(new { Message = "Invalid station ID format." });

            var result = await _mediator.Send(new DeleteStationCommand(stationId));
            return StatusCode(result.StatusCode, result);
        }
    }
}
