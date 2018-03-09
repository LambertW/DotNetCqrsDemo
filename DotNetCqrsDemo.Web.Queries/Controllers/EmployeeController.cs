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
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get Employee By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(EmployeeRM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByID(int id)
        {
            var employee = await _employeeRepository.GetByID(id);

            if(employee == null)
            {
                return NotFound(id);
            }

            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAll();
            return Ok(employees);
        }

    }
}