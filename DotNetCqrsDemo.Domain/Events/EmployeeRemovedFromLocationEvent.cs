using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Events
{
    public class EmployeeRemovedFromLocationEvent : BaseEvent
    {
        public readonly int OldLocationID;
        public readonly int EmployeeID;

        public EmployeeRemovedFromLocationEvent(Guid id, int oldLocationID, int employeeID)
        {
            Id = id;
            OldLocationID = oldLocationID;
            EmployeeID = employeeID;
        }
    }
}
