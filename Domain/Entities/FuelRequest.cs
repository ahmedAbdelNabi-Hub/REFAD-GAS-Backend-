using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Entities
{
    public class FuelRequest : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Guid CarId { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string Station { get; set; }
        public DateTimeOffset RequestDateTime { get; set; } = DateTimeOffset.UtcNow;
        public string Status { get; set; } = "pending";
        public Guid? StationId { get; set; }
        public string PumpImageBefore { get; set; }
        public string PumpImageAfter { get; set; }
    }
}
