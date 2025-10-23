using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Cars
{
    public class CarCountSpecification : CarsSpecification
    {
        public CarCountSpecification(PaginationParams paginationParams, CarParams carParams)
            : base(paginationParams, carParams)
        {

        }
    }
}
