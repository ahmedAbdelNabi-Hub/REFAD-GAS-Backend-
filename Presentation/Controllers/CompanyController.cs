using Application.DTOs.Company;
using Application.DTOs.Response;
using Application.UseCases.Company.Commands.CreateCompany;
using Application.UseCases.Company.Commands.UpdateCompany;
using Application.UseCases.Company.Commands.UpdateCompanyStatus;
using Application.UseCases.Company.Queries.GetAllCompanies;
using Application.UseCases.Company.Queries.GetCompanyById;
using Application.UseCases.Company.Queries.GetCompanyDropdown;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/api/companies/{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var companyId))
                return BadRequest(new { Message = "Invalid company ID format." });

            var result = await _mediator.Send(new GetCompanyByIdQuery(companyId));
            if (result == null)
                return NotFound(new { Message = "Company not found." });
            return Ok(result);
        }

        [HttpGet("/api/companies/dropdown")]
        public async Task<IActionResult> GetCompanyDropdown()
        {
            var companies = await _mediator.Send(new GetCompanyDropdownQuery());
            return Ok(companies); 
        }

       
        [HttpPost("/api/companies")]
        public async Task<IActionResult> CreateCompany([FromForm] CreateCompanyDto companyDto)
        {
            if (companyDto == null)
                return BadRequest(new BaseApiResponse<int>(400, "Invalid company data."));
            var command = new CreateCompanyCommand(companyDto);
            var result = await _mediator.Send(command);

            if (result.StatusCode != 200)
                return StatusCode(result.StatusCode, result);
            return Ok(result);
        }

        [HttpPut("/api/companies/{id}")]
        public async Task<IActionResult> UpdateCompany([FromRoute] string id, [FromForm] UpdateCompanyDto dto)
        {
            if (!Guid.TryParse(id, out var companyId))
                return BadRequest(new { message = "Invalid company ID format." });

            var result = await _mediator.Send(new UpdateCompanyCommand(companyId, dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("/api/companies/{id}/status")]
        public async Task<IActionResult> UpdateCompanyStatus([FromRoute] string id, [FromBody] UpdateCompanyStatusDto dto)
        {
            if (!Guid.TryParse(id, out var companyId))
                return BadRequest(new { Message = "Invalid company ID format." });

            var result = await _mediator.Send(new UpdateCompanyStatusCommand(companyId, dto));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/companies")]
        public async Task<IActionResult> GetAllCompanies([FromQuery(Name = "")] PaginationParams paginationParams, [FromQuery(Name = "")] CompanyParams companyParams)
        {
            var query = new GetAllCompaniesQuery(paginationParams, companyParams);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
