using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Notify
{
    public class NotifyModel
    {
        public int NotifyId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
