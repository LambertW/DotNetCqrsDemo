using CQRSlite.Commands;
using CQRSlite.Domain;
using DotNetCqrsDemo.Domain.Commands;
using DotNetCqrsDemo.Domain.WriteModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.CommandHandlers
{
    public class EmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
    {
        private readonly ISession _session;

        public EmployeeCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateEmployeeCommand command)
        {
            Employee employee = new Employee(command.Id, command.EmployeeID, command.FirstName, command.LastName, command.DateOfBirth, command.JobTitle);
            await _session.Add(employee);
            await _session.Commit();
        }
    }
}
