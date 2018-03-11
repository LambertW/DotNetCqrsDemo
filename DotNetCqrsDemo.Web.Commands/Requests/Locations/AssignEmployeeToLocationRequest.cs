using DotNetCqrsDemo.Domain.ReadModel.Repositories;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Web.Commands.Requests.Locations
{
    public class AssignEmployeeToLocationRequest
    {
        public int LocationID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class AssignEmployeeToLocationRequestValidator : AbstractValidator<AssignEmployeeToLocationRequest>
    {
        public AssignEmployeeToLocationRequestValidator(
            ILocationRepository locationRepository,
            IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.LocationID)
                .Must(x => locationRepository.Exists(x)).WithMessage("No location with this ID exists.");
            RuleFor(x => x.EmployeeID)
                .Must(x => employeeRepository.Exists(x)).WithMessage("No Employee with this ID exists.");
            RuleFor(x => new { x.LocationID, x.EmployeeID })
                .Must(x => !(locationRepository.HasEmployee(x.LocationID, x.EmployeeID)).Result).WithMessage("This Employee is already assigned to that Location.");
        }
    }
}
