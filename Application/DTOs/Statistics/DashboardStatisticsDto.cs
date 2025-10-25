using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Statistics
{
    public class DashboardStatisticsDto
    {
        public int TotalCompanies { get; set; }
        public int ActiveCompanies { get; set; }
        public int SuspendedCompanies { get; set; }

        public int ActiveVendors { get; set; }
        public int SuspendedVendors { get; set; }

        public int TotalCars { get; set; }
        public int ActiveCars { get; set; }
        public int InactiveCars { get; set; }

        public int TotalVendors { get; set; }
        public int TotalStations { get; set; }

        public decimal TotalPayments { get; set; }
        public int PaidPayments { get; set; }
        public int PendingPayments { get; set; }
    }
}
