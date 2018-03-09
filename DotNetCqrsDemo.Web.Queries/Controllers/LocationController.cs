using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCqrsDemo.Domain.ReadModel;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsDemo.Web.Queries.Controllers
{
    [Produces("application/json")]
    [Route("api/Location")]
    public class LocationController : Controller
    {
        private ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(LocationRM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByID(int id)
        {
            var location = await _locationRepository.GetByID(id);
            if(location == null)
            {
                return NotFound(id);
            }

            return Ok(location);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationRM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var location = await _locationRepository.GetAll();
            return Ok(location);
        }

        [HttpGet]
        [Route("{id}/employees")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeRM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees(int id)
        {
            var employees = await _locationRepository.GetEmployees(id);
            return Ok(employees);
        }
    }
}