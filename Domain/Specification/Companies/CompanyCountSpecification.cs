using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Companies
{
    public class CompanyCountSpecification : CompanySpecifications
    {
        public CompanyCountSpecification(PaginationParams paginationParams, CompanyParams companyParams)
            : base(paginationParams, companyParams)
        {
        }
    }
}
