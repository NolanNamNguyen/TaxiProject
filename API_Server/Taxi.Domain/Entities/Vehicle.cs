using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string License { get; set; }
        public string VehicleName { get; set; }
        public int Seater { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
