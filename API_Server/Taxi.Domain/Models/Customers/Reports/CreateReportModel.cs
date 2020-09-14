using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Customers.Reports
{
    public class CreateReportModel
    {
        public string Content { get; set; }
        public int DriverId { get; set; }

    }
}
