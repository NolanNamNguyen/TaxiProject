using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Taxi.Domain.Models.Customers.orders
{
    public class SearchScheduleModel
    {
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        [DefaultValue(1)]
        public int Reservations { get; set; }
    }
}
