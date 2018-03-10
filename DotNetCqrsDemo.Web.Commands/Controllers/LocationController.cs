using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Commands;
using DotNetCqrsDemo.Domain.Commands;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using DotNetCqrsDemo.Web.Commands.Requests.Locations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsDemo.Web.Commands.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationController : Controller
    {
        private readonly IMapper _mapper;
        private ICommandSender _commandSender;
        private ILocationRepository _locationRepository;
        private IEmployeeRepository _employeeRepository;

        public LocationController(IMapper mapper, ICommandSender commandSender, ILocationRepository locationRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _commandSender = commandSender;
            _locationRepository = locationRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateLocationRequest request)
        {
            var command = _mapper.Map<CreateLocationCommand>(request);
            await _commandSender.Send(command);

            return Ok();
        }
    }
}