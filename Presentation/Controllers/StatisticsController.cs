using Application.UseCases.Statistics.Queries.GetAllCount;
using Application.UseCases.Statistics.Queries.GetFuelRequestsChart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/api/statistics/count")]
    public async Task<IActionResult> GetDashboardStatistics()
    {
        var result = await _mediator.Send(new GetDashboardStatisticsQuery());
        return Ok(result);
    }
    [HttpGet("/api/statistics/fuel-chart")]
    public async Task<IActionResult> GetFuelRequestsChart([FromQuery] string periodType = "month", [FromQuery] int year = 0)
    {
        var query = new GetFuelRequestsChartQuery
        {
            PeriodType = periodType,
            Year = year == 0 ? DateTime.UtcNow.Year : year
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
}