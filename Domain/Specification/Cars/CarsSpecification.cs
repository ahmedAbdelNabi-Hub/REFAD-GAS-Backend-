using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Cars
{
    public class CarsSpecification : BaseSpecifications<Car>
    {
        public CarsSpecification(PaginationParams paginationParams, CarParams carParams)
        {
            Expression<Func<Car, bool>> criteria = c => true;

            if (!string.IsNullOrWhiteSpace(carParams.Status))
                criteria = criteria.AndAlso(c => c.Status.ToLower() == carParams.Status.ToLower());

            if (!string.IsNullOrWhiteSpace(carParams.CompanyId))
                criteria = criteria.AndAlso(c => c.CompanyId.ToString() == carParams.CompanyId);


            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.Trim().ToLowerInvariant();
                criteria = criteria.AndAlso(u =>
                    (u.PlateNumber != null && u.PlateNumber.ToLower().Contains(s)) ||
                    (u.DriverName != null && u.DriverName.ToLower().Contains(s)) ||
                    (u.CarType != null && u.CarType.ToLower().Contains(s)) ||
                    (u.DriverMobile != null && u.DriverMobile.ToLower().Contains(s)) 
                );
            }
            AddCriteria(criteria);
        }

        public CarsSpecification(string plateNumber) :base(c=>c.PlateNumber== plateNumber) { }  
     
        public CarsSpecification(Guid carId) : base(c=> c.Id.CompareTo(carId) == 0) { }
    }
}
