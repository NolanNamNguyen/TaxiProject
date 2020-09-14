using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Drivers
{
    public class DriverModel
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string License { get; set; }
        public string VehicleName { get; set; }
        public string ImagePath { get; set; }
        public float rateAverage { get; set; }
    }
}
