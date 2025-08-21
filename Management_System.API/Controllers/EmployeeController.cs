using Microsoft.AspNetCore.Mvc;
using NTier.Management_System.API.DTO.EmployeeDTO;
using NTier.ManagementSystem.Domain.Entities;
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
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employee);
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
    }
}
