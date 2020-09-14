using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Customers.orders
{
    public class AddOrderModel
    {
        [Required]
        public string PickupLocation { get; set; }
        [Required]
        public string ReturnLocation { get; set; }
        [DefaultValue(1)]
        public int Reservations { get; set; }
        [Required]
        public int DriverId { get; set; }
    }
}
