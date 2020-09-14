using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Taxi.Domain.Models.Users
{
    public class VerifyEmailRequestModel
    {
        [Required]
        public string Token { get; set; }
    }
}
