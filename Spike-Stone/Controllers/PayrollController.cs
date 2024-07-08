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

        [HttpGet("employeeId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] int employeeId)
        {
            var result = await _payrollService.GetPayCheck(employeeId);            
            return Ok(result);
        }
    }
}
