using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Companies
{
    public class CompanySpecifications : BaseSpecifications<Company>
    {
        public CompanySpecifications(PaginationParams paginationParams, CompanyParams companyParams)
        {
            Expression<Func<Company, bool>> criteria = c => true;

            if (!string.IsNullOrWhiteSpace(companyParams.Status))
                criteria = criteria.AndAlso(c => c.Status.ToLower() == companyParams.Status.ToLower());
            
            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.Trim().ToLowerInvariant();
                criteria = criteria.AndAlso(u =>
                    (u.CompanyNameArabic != null && u.CompanyNameArabic.ToLower().Contains(s)) ||
                    (u.CompanyNameEnglish != null && u.CompanyNameEnglish.ToLower().Contains(s)) ||
                    (u.IdentityId != null && u.IdentityId.ToLower().Contains(s)) ||
                    (u.CompanyNameArabic != null && u.CompanyNameArabic.ToLower().Contains(s)) ||
                    (u.ResponsiblePerson != null && u.ResponsiblePerson.ToLower().Contains(s)) ||
                    (u.Email != null && u.Email.ToLower().Contains(s)) 
                );
            }
            AddCriteria(criteria);
        }

        public CompanySpecifications(string email)
        {
            AddCriteria(c=>c.Email==email); 
        }
    }
}
