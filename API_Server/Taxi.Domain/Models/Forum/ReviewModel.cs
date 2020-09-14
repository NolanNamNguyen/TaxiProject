using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Forum
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int? DriverId { get; set; }
        public int? CustomerId { get; set; }
        public int Likes { get; set; }
        public int Cmts { get; set; }
    }
}
