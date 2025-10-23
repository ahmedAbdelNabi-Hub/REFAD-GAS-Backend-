using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction
{
    public static class AdminRoles
    {
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";

        public static readonly string[] All = { Admin, SuperAdmin };
    }
}
