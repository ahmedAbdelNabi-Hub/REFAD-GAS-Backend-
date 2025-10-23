using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vendors
{
    public class VendorReadDto
    {
        public Guid Id { get; set; }
        public string VendorCode { get; set; }
        public string VendorNameEn { get; set; }
        public string VendorNameAr { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string HeadquartersAddress { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
        public int StationCount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
