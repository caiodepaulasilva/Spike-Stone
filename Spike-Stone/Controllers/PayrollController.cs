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

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] int id, DateTime date)
        {
            var result = await _payrollService.GetPayCheck(id, date);            
            return Ok(result);
        }
    }
}
