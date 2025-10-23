using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification
{
    public class RefreshTokenSpecifications : BaseSpecifications<RefreshToken>
    {
        public RefreshTokenSpecifications(string token) : base(r=>r.Token==token)
        {
            
        }


    }
}
