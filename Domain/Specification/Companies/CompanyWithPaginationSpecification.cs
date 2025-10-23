using Domain.Entities;
using Domain.Specification.Params;

namespace Domain.Specification.Companies
{
    public class CompanyWithPaginationSpecification : CompanySpecifications
    {
        public CompanyWithPaginationSpecification(PaginationParams paginationParams, CompanyParams companyParams)
            : base(paginationParams, companyParams)
        {
            ApplyPagination(
                (paginationParams.PageIndex - 1) * paginationParams.PageSize,
                paginationParams.PageSize
            );
        }
    }
}
