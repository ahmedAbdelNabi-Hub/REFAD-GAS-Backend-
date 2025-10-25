using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.FuelRequests
{
    public class FuelRequestWithPaginationsSpecification : FuelRequestSpecification 
    {
        public FuelRequestWithPaginationsSpecification(PaginationParams paginationParams, FuelRequestParams fuelParams)
           : base(paginationParams, fuelParams)
        {
            ApplyPagination(
                (paginationParams.PageIndex - 1) * paginationParams.PageSize,
                paginationParams.PageSize
            );
            
        }
    }
}
