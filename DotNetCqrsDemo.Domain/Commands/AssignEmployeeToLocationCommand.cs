using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Commands
{
    public class AssignEmployeeToLocationCommand : BaseCommand
    {
        public readonly int LocationID;
        public readonly int EmployeeID;

        public AssignEmployeeToLocationCommand(Guid id, int newLocationID, int employeeID)
        {
            Id = id;
            LocationID = newLocationID;
            EmployeeID = employeeID;
        }
    }
}
