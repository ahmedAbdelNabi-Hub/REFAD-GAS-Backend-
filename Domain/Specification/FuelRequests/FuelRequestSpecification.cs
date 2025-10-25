using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.FuelRequests
{
    public class FuelRequestSpecification : BaseSpecifications<FuelRequest>
    {
        public FuelRequestSpecification(PaginationParams paginationParams, FuelRequestParams fuelRequestParams)
        {
            Expression<Func<FuelRequest, bool>> criteria = c => true;

            if (!string.IsNullOrWhiteSpace(fuelRequestParams.Status))
                criteria = criteria.AndAlso(c => c.Status.ToLower() == fuelRequestParams.Status.ToLower());


            if (!string.IsNullOrWhiteSpace(fuelRequestParams.CompanyId))
                criteria = criteria.AndAlso(c => c.CompanyId == Guid.Parse(fuelRequestParams.CompanyId));

            if (!string.IsNullOrWhiteSpace(fuelRequestParams.CarId))
                criteria = criteria.AndAlso(c => c.CarId == Guid.Parse(fuelRequestParams.CarId));

            if (!string.IsNullOrWhiteSpace(fuelRequestParams.StationId))
                criteria = criteria.AndAlso(c => c.StationId == Guid.Parse(fuelRequestParams.StationId));


            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.Trim().ToLowerInvariant();
                criteria = criteria.AndAlso(u =>
                    (u.Qty.ToString() != null && u.Qty.ToString().ToLower().Contains(s)) ||
                    (u.Amount.ToString() != null && u.Amount.ToString().ToLower().Contains(s))  ||
                    (u.Car.PlateNumber.ToString().ToLower().Contains(s)) || (u.Station.StationNameAr.ToString().ToLower().Contains(s) || u.Station.StationNameEn.ToString().ToLower().Contains(s))

                );
            }
            AddCriteria(criteria);
            AddOrderBy(f => f.CreatedAt);
            AddInclude(f => f.Car);
            AddInclude(f => f.Station);

        }

    }
}
