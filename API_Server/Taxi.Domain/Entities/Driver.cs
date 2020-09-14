using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        
    }
}
