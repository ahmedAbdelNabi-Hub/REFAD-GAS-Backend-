using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Station : BaseEntity
    {
        public string StationId { get; set; }
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public double LocationLat { get; set; }
        public double LocationLng { get; set; }
        public string StationNameAr { get; set; }
        public string StationNameEn { get; set; }
        public ICollection<FuelRequest> FuelRequests { get; set; }
    }
}
