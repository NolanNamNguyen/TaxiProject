using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Customers.Reports
{
    public class ReportModel
    {
        public int ReportId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
