using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Forum
{
    public class AddReviewModel
    {
        [Required]
        public string Content { get; set; }
    }
}
