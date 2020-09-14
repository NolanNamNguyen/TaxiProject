using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Drivers
{
    public class DriverReviewModel
    {
        public int Rate { get; set; }
        public string RateContent { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public DateTime Created { get; set; }
    }
}
