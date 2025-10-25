using Application.DTOs.Statistics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries.GetFuelRequestsChart
{
    public class GetFuelRequestsChartQuery : IRequest<List<FuelRequestChartDto>>
    {
        public string PeriodType { get; set; } = "month"; 
        public int Year { get; set; } = DateTime.UtcNow.Year;
    }
}
