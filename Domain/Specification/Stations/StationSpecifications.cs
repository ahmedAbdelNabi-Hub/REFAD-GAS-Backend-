using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System.Linq.Expressions;

namespace Domain.Specification.Stations
{
    public class StationSpecifications : BaseSpecifications<Station>
    {
        public StationSpecifications(PaginationParams paginationParams)
        {
            Expression<Func<Station, bool>> criteria = s => true;

            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var search = paginationParams.Search.Trim().ToLowerInvariant();

                criteria = criteria.AndAlso(s =>
                    (s.StationId != null && s.StationId.ToLower().Contains(search)) ||
                    (s.StationNameAr != null && s.StationNameAr.ToLower().Contains(search)) ||
                    (s.StationNameEn != null && s.StationNameEn.ToLower().Contains(search)) ||
                    (s.Vendor != null && s.Vendor.VendorNameAr.ToLower().Contains(search)) ||
                    (s.Vendor != null && s.Vendor.VendorNameEn.ToLower().Contains(search))
                );
            }

            AddCriteria(criteria);
        }

        public StationSpecifications(string stationId)
        {
            AddCriteria(s => s.StationId == stationId);
        }
    }
}
