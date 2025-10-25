using Application.DTOs.Statistics;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices; // Required for FormattableStringFactory

namespace Application.UseCases.Statistics.Queries.GetFuelRequestsChart
{
    public class GetFuelRequestsChartQueryHandler : IRequestHandler<GetFuelRequestsChartQuery, List<FuelRequestChartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFuelRequestsChartQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FuelRequestChartDto>> Handle(GetFuelRequestsChartQuery request, CancellationToken cancellationToken)
        {
            FormattableString query;

            if (request.PeriodType.Equals("day", StringComparison.OrdinalIgnoreCase))
            {
                query = FormattableStringFactory.Create(@$"
                    SELECT 
                        FORMAT(request_datetime, 'yyyy-MM-dd') AS Label,
                        SUM(amount) AS TotalAmount
                    FROM fuel_requests
                    WHERE YEAR(request_datetime) = {request.Year}
                    GROUP BY FORMAT(request_datetime, 'yyyy-MM-dd')
                    ORDER BY Label
                ");
            }
            else if (request.PeriodType.Equals("week", StringComparison.OrdinalIgnoreCase))
            {
                query = FormattableStringFactory.Create(@$"
                    SELECT 
                        CONCAT(N'الأسبوع ', DATEPART(WEEK, request_datetime)) AS Label,
                        SUM(amount) AS TotalAmount
                    FROM fuel_requests
                    WHERE YEAR(request_datetime) = {request.Year}
                    GROUP BY DATEPART(WEEK, request_datetime)
                    ORDER BY DATEPART(WEEK, request_datetime)
                ");
            }
            else 
            {
                query = FormattableStringFactory.Create(@$"
                    SELECT 
                        DATENAME(MONTH, request_datetime) AS Label,
                        SUM(amount) AS TotalAmount
                    FROM fuel_requests
                    WHERE YEAR(request_datetime) = {request.Year}
                    GROUP BY DATENAME(MONTH, request_datetime), MONTH(request_datetime)
                    ORDER BY MONTH(request_datetime)
                ");
            }

            var result = await _unitOfWork.ExecuteSqlInterpolatedAsync<FuelRequestChartDto>(query);
            return result.ToList();
        }
    }
}
