using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CompanyBalance : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal FuelBalance { get; set; }
        public decimal ServicesBalance { get; set; }

        public Company Company { get; set; }
    }
}
