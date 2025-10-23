using Domain.Entities;
using Domain.Specification.Helper;
using Domain.Specification.Params;
using System;
using System.Linq.Expressions;

namespace Domain.Specification.Admins
{
    public class AdminSpecifications : BaseSpecifications<AdminUser>
    {
        public AdminSpecifications(PaginationParams paginationParams, AdminParams adminParams)
        {
            Expression<Func<AdminUser, bool>> criteria = a => true;

            if (!string.IsNullOrWhiteSpace(adminParams.Role))
                criteria = criteria.AndAlso(a => a.Role.ToLower() == adminParams.Role.ToLower());

            if (adminParams.IsActive.HasValue)
                criteria = criteria.AndAlso(a => a.IsActive == adminParams.IsActive.Value);

            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.Trim().ToLowerInvariant();
                criteria = criteria.AndAlso(a =>
                    (a.FullName != null && a.FullName.ToLower().Contains(s)) ||
                    (a.Email != null && a.Email.ToLower().Contains(s)) ||
                    (a.Role != null && a.Role.ToLower().Contains(s))
                );
            }

            AddCriteria(criteria);
        }

        public AdminSpecifications(string email)
        {
            AddCriteria(a => a.Email == email);
        }
        public AdminSpecifications(Guid id)
        {
            AddCriteria(a => a.Id == id);
        }
    }
}
