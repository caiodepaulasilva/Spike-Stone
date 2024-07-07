using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spike_Stone.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);            
            return Created(nameof(Get), result);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _employeeService.GetEmployee(id);
            return Ok(result);
        }
    }
}
