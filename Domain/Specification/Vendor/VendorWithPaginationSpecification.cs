using Domain.Specification.Params;
using Domain.Specification.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Vendor
{
    public class VendorWithPaginationSpecification : VendorSpecifications
    {
        public VendorWithPaginationSpecification(PaginationParams paginationParams, VendorParams vendorParams): base(paginationParams, vendorParams)
        {
            ApplyPagination(
                (paginationParams.PageIndex - 1) * paginationParams.PageSize,
                paginationParams.PageSize
            );
        }
    }
}
