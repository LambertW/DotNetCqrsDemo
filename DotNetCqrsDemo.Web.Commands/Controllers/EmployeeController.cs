using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Commands;
using DotNetCqrsDemo.Domain.Commands;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using DotNetCqrsDemo.Web.Commands.Requests.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsDemo.Web.Commands.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private ICommandSender _commandSender;
        private readonly IMapper _mapper;
        private ILocationRepository _locationRepository;

        public EmployeeController(ICommandSender commandSender, IMapper mapper, ILocationRepository locationRepository)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateEmployeeRequest request)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(request);
            await _commandSender.Send(command);

            var locationAggregateID = (await _locationRepository.GetByID(request.LocationID)).AggregateID;

            var assignCommand = new AssignEmployeeToLocationCommand(locationAggregateID, request.LocationID, request.EmployeeID);
            await _commandSender.Send(assignCommand);

            return Ok();
        }
    }
}