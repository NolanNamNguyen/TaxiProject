using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
