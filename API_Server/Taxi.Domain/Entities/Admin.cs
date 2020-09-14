using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
