﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeService.Models
{
    public partial class Title
    {
        public int EmpNo { get; set; }
        public string Title1 { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; }
    }
}
