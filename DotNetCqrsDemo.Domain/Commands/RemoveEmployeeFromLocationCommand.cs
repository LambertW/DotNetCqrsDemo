using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Commands
{
    public class RemoveEmployeeFromLocationCommand : BaseCommand
    {
        public readonly int LocationID;
        public readonly int EmployeeID;

        public RemoveEmployeeFromLocationCommand(Guid id, int locationID, int employeeID)
        {
            Id = id;
            LocationID = locationID;
            EmployeeID = employeeID;
        }
    }
}
