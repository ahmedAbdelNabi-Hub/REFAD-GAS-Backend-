using Application.DTOs.Statistics;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Application.UseCases.Statistics.Queries.GetAllCount
{
    public class GetDashboardStatisticsQueryHandler : IRequestHandler<GetDashboardStatisticsQuery, DashboardStatisticsDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardStatisticsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardStatisticsDto> Handle(GetDashboardStatisticsQuery request, CancellationToken cancellationToken)
        {
            var sql = FormattableStringFactory.Create(@"
              SELECT
                (SELECT COUNT(*) FROM companies) AS TotalCompanies,
                (SELECT COUNT(*) FROM companies WHERE status = 'active') AS ActiveCompanies,
                (SELECT COUNT(*) FROM companies WHERE status = 'suspended') AS SuspendedCompanies,

                (SELECT COUNT(*) FROM cars) AS TotalCars,
                (SELECT COUNT(*) FROM cars WHERE status = 'active') AS ActiveCars,
                (SELECT COUNT(*) FROM cars WHERE status = 'inactive') AS InactiveCars,

                (SELECT COUNT(*) FROM vendors) AS TotalVendors,
                (SELECT COUNT(*) FROM vendors WHERE is_active = 1) AS ActiveVendors,
                (SELECT COUNT(*) FROM vendors WHERE is_active = 0) AS SuspendedVendors,

                (SELECT COUNT(*) FROM stations) AS TotalStations,

                (SELECT ISNULL(SUM(amount), 0) FROM payments) AS TotalPayments,
                (SELECT COUNT(*) FROM payments WHERE status = 'Approved') AS PaidPayments,
                (SELECT COUNT(*) FROM payments WHERE status = 'Pending') AS PendingPayments
            ");

            var result = await _unitOfWork.ExecuteSqlInterpolatedAsync<DashboardStatisticsDto>(sql);

            return result.FirstOrDefault() ?? new DashboardStatisticsDto();
        }
    }
}