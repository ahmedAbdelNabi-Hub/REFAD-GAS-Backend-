using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Stations
{
    public class StationCountSpecification : StationSpecifications
    {
        public StationCountSpecification(PaginationParams paginationParams): base(paginationParams)
        {

        }
    }
}
