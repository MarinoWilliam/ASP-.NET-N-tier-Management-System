using Microsoft.AspNetCore.Mvc;
using NTier.ManagementSystem.Service.Interfaces;
using NTier.ManagementSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace NTier.Management_System.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
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
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null)
                    return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Department is required.");
                }

                var createdDepartment = await _departmentService.AddDepartmentAsync(department);
                return CreatedAtAction(nameof(Get), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department department)
        {
            try
            {
                if (department == null || department.Id != id)
                {
                    return BadRequest("Invalid department data.");
                }

                var updatedDepartment = await _departmentService.UpdateDepartmentAsync(department);
                if (updatedDepartment == null)
                    return NotFound();

                return Ok(updatedDepartment);
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
                var success = await _departmentService.DeleteDepartmentByIdAsync(id);
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
