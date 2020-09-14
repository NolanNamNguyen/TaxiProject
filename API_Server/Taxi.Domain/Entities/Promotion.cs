using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string Content { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime FinishDay { get; set; }
        public DateTime Created { get; set; }
    }
}
