using Domain.Specification.Admins;
using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Admin
{
    public class AdminCountSpecification : AdminSpecifications
    {
        public AdminCountSpecification(PaginationParams paginationParams, AdminParams adminParams)
          : base(paginationParams, adminParams)
        {
           
        }
    }
}
