using Microsoft.AspNetCore.Mvc;
using NTier.ManagementSystem.Data.DTO.EmployeeDTO;
using NTier.ManagementSystem.Data.Entities;
using NTier.ManagementSystem.Service.Implementations;
using NTier.ManagementSystem.Service.Interfaces;

namespace NTier.Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmpoyeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null) return NotFound();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto employeeDto)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                if (updatedEmployee == null) return NotFound();
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employeeDto)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeDto.FirstName) || string.IsNullOrEmpty(employeeDto.LastName))
                {
                    return BadRequest("First name and last name are required.");
                }

                var employee = await _employeeService.AddEmployeeAsync(
                    employeeDto.FirstName,
                    employeeDto.LastName,
                    employeeDto.Type,
                    employeeDto.Email,
                    employeeDto.DepartmentId,
                    employeeDto.TeamId,
                    employeeDto.AnnualSalary,
                    employeeDto.ContractAgency,
                    employeeDto.HourlyRate
                );

                return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _employeeService.DeleteEmployeeByIdAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
