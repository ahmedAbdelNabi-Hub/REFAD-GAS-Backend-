using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.FuelRequests
{
    public class FuelRequestWithCountSpecification : FuelRequestSpecification
    {
        public FuelRequestWithCountSpecification(PaginationParams paginationParams, FuelRequestParams fuelParams): base(paginationParams, fuelParams)
        {
           
        }
    }
}
