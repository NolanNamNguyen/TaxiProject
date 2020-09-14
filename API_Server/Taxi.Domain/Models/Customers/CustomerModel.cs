﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Models.Customers
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
    }
}
