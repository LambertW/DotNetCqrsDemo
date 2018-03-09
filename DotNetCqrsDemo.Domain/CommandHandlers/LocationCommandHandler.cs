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
    public class LocationCommandHandler : 
        ICommandHandler<CreateLocationCommand>,
        ICommandHandler<AssignEmployeeToLocationCommand>,
        ICommandHandler<RemoveEmployeeFromLocationCommand>
    {
        private readonly ISession _session;

        public LocationCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateLocationCommand message)
        {
            var location = new Location(message.Id, message.LocationID, message.StreetAddress, message.City, message.State, message.PostalCode);
            await _session.Add(location);
            await _session.Commit();
        }

        public async Task Handle(AssignEmployeeToLocationCommand message)
        {
            Location location = await _session.Get<Location>(message.Id);
            location.AddEmployee(message.EmployeeID);

            await _session.Commit();
        }

        public async Task Handle(RemoveEmployeeFromLocationCommand message)
        {
            Location location = await _session.Get<Location>(message.Id);
            location.RemoveEmployee(message.EmployeeID);

            await _session.Commit();
        }
    }
}
