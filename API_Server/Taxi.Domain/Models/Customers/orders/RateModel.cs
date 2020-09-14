using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Customers.orders
{
    public class RateModel
    {
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        public string RateContent { get; set; }
    }
}
