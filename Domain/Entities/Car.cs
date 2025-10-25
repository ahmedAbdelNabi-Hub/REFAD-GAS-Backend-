using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string PlateNumber { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; } 
        public string Status { get; set; } = "inactive";
        public string ControlType { get; set; } = "Monthly";
        public int LimitQty { get; set; }
        public string? StartDay { get; set; }
        public decimal UsedQty { get; set; } = 0;
        public string DriverName { get; set; }
        public string DriverMobile { get; set; }
        public string DriverPassword { get; set; }
        public string DriverImageUrl { get; set; }
        public Company Company { get; set; } 
        public ICollection<FuelRequest> FuelRequests { get; set; }
    }
}
