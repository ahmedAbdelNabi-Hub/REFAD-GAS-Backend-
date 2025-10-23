using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Station
{
    public class StationDto
    {
        public Guid Id { get; set; }
        public Guid VendorId {  get; set; }
        public string StationId { get; set; }
        public string StationNameAr { get; set; }
        public string StationNameEn { get; set; }
        public double LocationLat { get; set; }
        public double LocationLng { get; set; }
        public string VendorNameAr { get; set; }
        public string VendorNameEn { get; set; }
        public int FuelRequestCount { get; set; }
    }
}
