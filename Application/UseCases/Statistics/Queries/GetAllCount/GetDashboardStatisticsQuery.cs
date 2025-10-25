using Application.DTOs.Statistics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Statistics.Queries.GetAllCount
{
    public class GetDashboardStatisticsQuery : IRequest<DashboardStatisticsDto>
    {
    }
}
