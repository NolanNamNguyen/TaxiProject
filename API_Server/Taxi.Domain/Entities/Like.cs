using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
    }
}
