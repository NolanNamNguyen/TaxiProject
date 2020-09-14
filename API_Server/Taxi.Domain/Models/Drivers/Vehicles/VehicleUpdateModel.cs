using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Drivers.Vehicles
{
    public class VehicleUpdateModel
    {

        public string License { get; set; }
        public string VehicleName { get; set; }
        [Range(2, 17)]
        public int Seater { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Video { get; set; }
    }
}
