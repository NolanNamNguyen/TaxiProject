using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string StartAddress { get; set; }
        public string StartProvince { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationProvince { get; set; }
        public int AvailablableSlot { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
