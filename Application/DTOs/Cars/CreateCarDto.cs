using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cars
{
    public class CreateCarDto
    {
        public Guid CompanyId { get; set; }
        public string PlateNumber { get; set; }
        public string CarType { get; set; }
        public string FuelType { get; set; }
        public string ControlType { get; set; } = "Monthly"; 
        public string? StartDay { get; set; } 
        public int LimitQty { get; set; }

        public string DriverName { get; set; }
        public string DriverMobile { get; set; }
        public string DriverPassword { get; set; }
        public IFormFile DriverImageUrl { get; set; }
    }
}
