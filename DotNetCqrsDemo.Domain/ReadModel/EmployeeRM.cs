﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.ReadModel
{
    public class EmployeeRM
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int LocationID { get; set; }
        public Guid AggregateID { get; set; }
    }
}
