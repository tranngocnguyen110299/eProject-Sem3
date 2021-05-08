﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFloralDelivery.Areas.Admin.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}