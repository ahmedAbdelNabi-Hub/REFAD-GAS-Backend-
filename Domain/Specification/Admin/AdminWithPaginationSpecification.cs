using Domain.Specification.Admins;
using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Admin
{
    public class AdminWithPaginationSpecification : AdminSpecifications
    {
        public AdminWithPaginationSpecification(PaginationParams paginationParams, AdminParams adminParams)
             : base(paginationParams, adminParams)
        {
            ApplyPagination(
                (paginationParams.PageIndex - 1) * paginationParams.PageSize,
                paginationParams.PageSize
            );
        }
    }
}
