using Domain.Specification.Params;
using Domain.Specification.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Vendor
{
    public class VendorCountSpecification : VendorSpecifications
    {
        public VendorCountSpecification(PaginationParams paginationParams, VendorParams vendorParams)
           : base(paginationParams, vendorParams)
        {
        }
    }
}
