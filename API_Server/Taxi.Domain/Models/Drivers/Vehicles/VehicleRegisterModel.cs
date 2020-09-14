using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Drivers.Vehicles
{
    public class VehicleRegisterModel
    {
        public int VehicleId { get; set; }
        [Required]
        public string License { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        [Range(2, 17)]
        public int Seater { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public IFormFile Video { get; set; }
        [Required]
        public int DriverId { get; set; }

    }
}
