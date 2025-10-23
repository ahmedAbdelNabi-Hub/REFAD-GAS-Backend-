using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyNameArabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string ResponsiblePerson { get; set; }
        public string IdentityId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }

        public bool PumpImageRequired { get; set; }
        public bool CarImageRequired { get; set; }
        public string CarLimitType { get; set; } = "monthly";
        public int CarLimitCount { get; set; }
        public decimal MonthlyCostPerCar { get; set; }
        public string Status { get; set; } = "pending"; // active | pending | suspended

        public string? LogoPath { get; set; }
        public string? DocumentsPaths { get; set; } // store JSON array as string


        // Relations
        public ICollection<Car> Cars { get; set; }
        public CompanyBalance Balance { get; set; }
        public ICollection<FuelRequest> FuelRequests { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
