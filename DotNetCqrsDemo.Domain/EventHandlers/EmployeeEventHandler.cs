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
    /// <summary>
    /// Employee Event Handler
    /// </summary>
    public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeEventHandler(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(EmployeeCreatedEvent message)
        {
            EmployeeRM employee = _mapper.Map<EmployeeRM>(message);
            await _employeeRepository.Save(employee);
        }
    }
}
