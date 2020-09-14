using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Notify
    {
        public int NotifyId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
