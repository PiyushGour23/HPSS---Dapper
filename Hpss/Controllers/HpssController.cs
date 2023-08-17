using Hpss.Data;
using Hpss.Interface;
using Hpss.Model;
using Hpss.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Hpss.Controllers
{
    [EnableCors]
    [EnableRateLimiting("FixedWindow")]
    [Route("api/[controller]")]
    [ApiController]
    public class HpssController : ControllerBase
    {
        private readonly IHpssRepository _hpssRepository;
        private readonly ILogger<HpssController> _logger;

        public HpssController(IHpssRepository hpssRepository, ILogger<HpssController> logger)
        {
            _hpssRepository = hpssRepository ?? throw new ArgumentNullException(nameof(hpssRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Hi Irshad Here");
                var data = await _hpssRepository.GetEmployees();
                if (data == null)
                {
                    return NotFound();
                }
                else 
                { 
                    return Ok(data); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> Add(Employees employees)
        {
            var data = await _hpssRepository.Create(employees);
            if (data == null)
            {
                return NotFound();
            }
            else { return Ok(data); }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Updates(Employees employees, int id)
        {
            var data = await _hpssRepository.Update(employees, id);
            if (data == null)
            {
                return NotFound();
            }
            else { return Ok(data); }
        }

        [HttpDelete("RemoveEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var data = await _hpssRepository.Delete(id);
            if (data == null)
            {
                return NotFound();
            }
            else { return Ok(data); }
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetAll(int CompanyId)
        {
            var data = await _hpssRepository.GetEmployeesbyId(CompanyId);
            if (data == null)
            {
                return NotFound();
            }
            else { return Ok(data); }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _hpssRepository.GetAllUsers();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

    }
}
