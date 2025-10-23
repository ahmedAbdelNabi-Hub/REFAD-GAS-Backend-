using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vendors
{
    public class VendorDto
    {
        public string VendorCode { get; set; }
        public string VendorNameEn { get; set; }
        public string VendorNameAr { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string HeadquartersAddress { get; set; }
        public string? Logo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
