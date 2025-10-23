using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Cars
{
    public class CarWithPaginationSpecification : CarsSpecification
    {
        public CarWithPaginationSpecification(PaginationParams paginationParams, CarParams carParams)
            : base(paginationParams, carParams)
        {
            ApplyPagination(
                (paginationParams.PageIndex - 1) * paginationParams.PageSize,
                paginationParams.PageSize
            );
            AddInclude(c => c.Company);


        }
    }
}
