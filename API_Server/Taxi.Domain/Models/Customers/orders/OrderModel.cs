using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Customers.orders
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public bool Status { get; set; }
        public bool IsCancel { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public string RateContent { get; set; }
        public DateTime Created { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverImg { get; set; }
        public float DriverRate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerImg { get; set; }
    }
}
