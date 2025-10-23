using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Company
{
    public class CompanyDetailsDto
    {
        public Guid Id { get; set; }
        public string CompanyNameArabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string ResponsiblePerson { get; set; }
        public string IdentityId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool PumpImageRequired { get; set; }
        public bool CarImageRequired { get; set; }
        public string CarLimitType { get; set; }
        public int CarLimitCount { get; set; }
        public decimal MonthlyCostPerCar { get; set; }
        public string Status { get; set; }
        public string? LogoPath { get; set; }
        public string? DocumentsPaths { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
