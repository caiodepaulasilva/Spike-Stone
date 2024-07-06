using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spike_Stone.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class PayrollController(IPayrollService payrollService) : ControllerBase
    {
        private readonly IPayrollService _payrollService = payrollService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            //var result = await _payrollService.AddProduct(productDto);
            var result = new { };
            return Created(nameof(Get), result);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            //var result = await _payrollService.GetProduct(id);
            var result = new { };
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            //var result = await _payrollService.GetProducts();
            var result = new { };
            return Ok(result);
        }
    }
}
