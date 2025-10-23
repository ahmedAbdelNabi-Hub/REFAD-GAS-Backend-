using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; } // Transfer | Deposit | Cash
        public string Status { get; set; } = "Pending"; // Approved | Declined | Pending
        public string ServiceType { get; set; } // Fuel | Services | Both
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public Company Company { get; set; }
    }
}
