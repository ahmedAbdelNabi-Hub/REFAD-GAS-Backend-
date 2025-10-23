using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System;
using System.Linq.Expressions;

namespace Domain.Specification.Vendors
{
    public class VendorSpecifications : BaseSpecifications<Domain.Entities.Vendor>
    {
        public VendorSpecifications(PaginationParams paginationParams, VendorParams vendorParams)
        {
            Expression<Func<Domain.Entities.Vendor, bool>> criteria = v => true;

            if (!string.IsNullOrWhiteSpace(vendorParams.Status))
                criteria = criteria.AndAlso(v => v.IsActive == (vendorParams.Status.ToLower() == "active"));

            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.Trim().ToLowerInvariant();
                criteria = criteria.AndAlso(v =>
                    (v.VendorCode != null && v.VendorCode.ToLower().Contains(s)) ||
                    (v.VendorNameAr != null && v.VendorNameAr.ToLower().Contains(s)) ||
                    (v.VendorNameEn != null && v.VendorNameEn.ToLower().Contains(s)) ||
                    (v.ContactEmail != null && v.ContactEmail.ToLower().Contains(s)) ||
                    (v.ContactPhone != null && v.ContactPhone.ToLower().Contains(s))
                );
            }

            AddCriteria(criteria);
        }

        public VendorSpecifications(string vendorCode)
        {
            AddCriteria(v => v.VendorCode == vendorCode);
        }
    }
}
