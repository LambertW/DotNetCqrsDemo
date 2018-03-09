using AutoMapper;
using CQRSlite.Events;
using DotNetCqrsDemo.Domain.Events;
using DotNetCqrsDemo.Domain.ReadModel;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.EventHandlers
{
    public class LocationEventHandler :
        IEventHandler<LocationCreatedEvent>,
        IEventHandler<EmployeeAssignedToLocationEvent>,
        IEventHandler<EmployeeRemovedFromLocationEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LocationEventHandler(IMapper mapper, ILocationRepository locationRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(LocationCreatedEvent message)
        {
            LocationRM location = _mapper.Map<LocationRM>(message);

            await _locationRepository.Save(location);
        }

        public async Task Handle(EmployeeAssignedToLocationEvent message)
        {
            var location = await _locationRepository.GetByID(message.NewLocationID);
            location.Employees.Add(message.EmployeeID);
            await _locationRepository.Save(location);

            var employee = await _employeeRepository.GetByID(message.EmployeeID);
            employee.LocationID = message.NewLocationID;
            await _employeeRepository.Save(employee);
        }

        public async Task Handle(EmployeeRemovedFromLocationEvent message)
        {
            var location = await _locationRepository.GetByID(message.OldLocationID);
            location.Employees.Remove(message.EmployeeID);
            await _locationRepository.Save(location);
        }
    }
}
