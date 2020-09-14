using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        //status == 1 => order have been done, opposite
        public bool Status { get; set; }
        public int Price { get; set; }
        //Number of seats reserved
        public int Reservations { get; set; }
        public bool IsCancel { get; set; }
        public int Rate { get; set; }
        public string RateContent { get; set; }
        public DateTime Created { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
