using Application.DTOs.Admins;
using Application.DTOs.Company;
using Application.DTOs.Response;
using Application.UseCases.Admin.Commands.ChangeAdminStatus;
using Application.UseCases.Admin.Commands.CreateAdmin;
using Application.UseCases.Admin.Commands.DeleteAdmin;
using Application.UseCases.Admin.Commands.UpdateAdmin;
using Application.UseCases.Admin.Queries.GetAdminById;
using Application.UseCases.Admin.Queries.GetAllAdmins;
using Application.UseCases.Car.Commands.CreateCar;
using Application.UseCases.Company.Commands.CreateCompany;
using Application.UseCases.Company.Commands.UpdateCompany;
using Application.UseCases.Company.Commands.UpdateCompanyStatus;
using Application.UseCases.Company.Queries.GetAllCompanies;
using Application.UseCases.Company.Queries.GetCompanyById;
using Application.UseCases.Company.Queries.GetCompanyDropdown;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupervisorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/supervisors")]
        public async Task<IActionResult> GetAllSupervisors([FromQuery(Name = "")] PaginationParams paginationParams, [FromQuery(Name = "")] AdminParams adminParams)
        {
            var query = new GetAllAdminsQuery(paginationParams, adminParams);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/api/supervisors/{id}")]
        public async Task<IActionResult> GetSupervisorById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var supervisorId))
                return BadRequest(new { Message = "Invalid supervisor ID format." });

            var result = await _mediator.Send(new GetAdminByIdQuery(supervisorId));
            if (result == null)
                return NotFound(new { Message = "supervisor not found." });
            return Ok(result.Data);
        }

        [HttpPost("/api/supervisors")]
        public async Task<IActionResult> CreateSupervisor([FromBody] CreateAdminCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.StatusCode != 200)
                return StatusCode(result.StatusCode, result);
            return Ok(result);
        }

        [HttpPut("/api/supervisors/{id}")]
        public async Task<IActionResult> UpdateSupervisor([FromRoute] string id, [FromBody] AdminDto dto)
        {
            if (!Guid.TryParse(id, out var supervisorId))
                return BadRequest(new { message = "Invalid supervisor ID format." });

            var result = await _mediator.Send(new UpdateAdminCommand(supervisorId, dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/supervisors/status")]
        public async Task<IActionResult> UpdateSupervisorStatus([FromBody] ChangeAdminStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("/api/supervisors/{id}")]
        public async Task<IActionResult> DeleteSupervisor([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var supervisorId))
                return BadRequest(new { message = "Invalid supervisor ID format." });

            var result = await _mediator.Send(new DeleteAdminCommand(supervisorId));
            return StatusCode(result.StatusCode, result);
        }

    }
}
