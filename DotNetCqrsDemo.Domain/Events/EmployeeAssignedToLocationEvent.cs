using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Events
{
    public class EmployeeAssignedToLocationEvent : BaseEvent
    {
        public readonly int NewLocationID;
        public readonly int EmployeeID;

        public EmployeeAssignedToLocationEvent(Guid id, int locationID, int employeeID)
        {
            Id = id;
            NewLocationID = locationID;
            EmployeeID = employeeID;
        }
    }
}
