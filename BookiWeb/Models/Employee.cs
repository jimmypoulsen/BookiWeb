﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiWeb.Models {
    public class Employee {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmployeeNo { get; set; }
        public string Title { get; set; }
        public int AccessLevel { get; set; }

    }
}