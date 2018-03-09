using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.ReadModel
{
    public class LocationRM
    {
        public int LocationID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public List<int> Employees { get; set; }
        public Guid AggregateID { get; set; }

        public LocationRM()
        {
            Employees = new List<int>();
        }
    }
}
