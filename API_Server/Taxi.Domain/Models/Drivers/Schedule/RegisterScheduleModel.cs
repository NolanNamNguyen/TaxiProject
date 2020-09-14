using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Drivers.Schedule
{
    public class RegisterScheduleModel
    {
        public string StartAddress { get; set; }
        [Required]
        public string StartProvince { get; set; }
        public string DestinationAddress { get; set; }
        [Required]
        public string DestinationProvince { get; set; }
        [Range(2, 17)]
        public int AvailablableSlot { get; set; }
        public DateTime StartTime { get; set; }
    }
}
