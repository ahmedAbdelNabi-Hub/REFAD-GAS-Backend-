using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Fuel
{
    public class FuelRequestBasicDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CarId { get; set; }
        public decimal Qty { get; set; }
        public string CompanyName { get; set; }
        public string CarPlateNumber { get; set; }
        public decimal Amount { get; set; }
        public string StationName { get; set; }
        public string CarName { get; set; }
        public string FuelType { get; set; }
        public string Status { get; set; }
        public DateTimeOffset RequestDate { get; set; } 
        public Guid? StationId { get; set; }
        public string PumpImageBefore { get; set; }
        public string PumpImageAfter { get; set; }
    }
}
