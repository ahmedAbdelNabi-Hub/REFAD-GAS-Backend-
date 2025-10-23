using Application.DTOs.Response;
using Application.DTOs.Vendors;
using Application.UseCases.Vendor.Commands.ChangeVendorStatus;
using Application.UseCases.Vendor.Commands.CreateVendor;
using Application.UseCases.Vendor.Commands.UpdateVendor;
using Application.UseCases.Vendor.Queries.GetAllVendors;
using Application.UseCases.Vendor.Queries.GetVendorById;
using Application.UseCases.Vendor.Queries.GetVendorDropdown;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/vendors")]
        public async Task<IActionResult> GetAllVendors([FromQuery(Name = "")] PaginationParams paginationParams, [FromQuery(Name = "")] VendorParams vendorParams)
        {
            var query = new GetAllVendorsQuery(paginationParams, vendorParams);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/api/vendors/{id}")]
        public async Task<IActionResult> GetVendorById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var vendorId))
                return BadRequest(new { Message = "Invalid vendor ID format." });

            var result = await _mediator.Send(new GetVendorByIdQuery(vendorId));
            if (result == null)
                return NotFound(new { Message = "Vendor not found." });

            return Ok(result.Data);
        }

        [HttpGet("/api/vendors/dropdown")]
        public async Task<IActionResult> GetVendorDropdown()
        {
            var vendors = await _mediator.Send(new GetVendorDropdownQuery());
            return Ok(vendors);
        }

        [HttpPost("/api/vendors")]
        public async Task<IActionResult> CreateVendor([FromBody] VendorDto dto)
        {
            if (dto == null)
                return BadRequest(new BaseApiResponse<int>(400, "Invalid vendor data."));

            var command = new CreateVendorCommand(dto);
            var result = await _mediator.Send(command);

            if (result.StatusCode != 200)
                return StatusCode(result.StatusCode, result);

            return Ok(result);
        }

        [HttpPut("/api/vendors/{id}")]
        public async Task<IActionResult> UpdateVendor([FromRoute] string id, [FromBody] VendorDto dto)
        {
            if (!Guid.TryParse(id, out var vendorId))
                return BadRequest(new { Message = "Invalid vendor ID format." });

            var result = await _mediator.Send(new UpdateVendorCommand(vendorId, dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/vendors/{id}/status")]
        public async Task<IActionResult> UpdateVendorStatus([FromRoute] string id, [FromBody] ChangeVendorStatusCommand dto)
        {
            if (!Guid.TryParse(id, out var vendorId))
                return BadRequest(new { Message = "Invalid vendor ID format." });

            var result = await _mediator.Send(new ChangeVendorStatusCommand(vendorId, dto.IsActive));
            return StatusCode(result.StatusCode, result);
        }

       
    }
}
