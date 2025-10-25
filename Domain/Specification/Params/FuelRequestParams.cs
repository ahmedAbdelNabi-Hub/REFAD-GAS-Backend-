using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Params
{
    public class FuelRequestParams
    {
        public string? CompanyId { get; set; }
        public string? StationId { get; set; }
        public string? CarId { get; set; }
        public string? Status { get; set; }

    }
}
