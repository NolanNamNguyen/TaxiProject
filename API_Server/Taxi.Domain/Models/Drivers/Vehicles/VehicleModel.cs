using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Models.Drivers.Vehicles
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }
        public string License { get; set; }
        public string VehicleName { get; set; }
        public int Seater { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
    }
}

