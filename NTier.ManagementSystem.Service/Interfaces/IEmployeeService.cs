using NTier.ManagementSystem.Data.Common.Enums;
using NTier.ManagementSystem.Data.DTO.EmployeeDTO;
using NTier.ManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<RetrunEmployeeDto>> GetAllEmpoyeesAsync();
        Task<RetrunEmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(
            string firstName,
            string lastName,
            EmployeeType type,
            string? email = null,
            int? departmentId = null,
            int? teamId = null,
            decimal? annualSalary = null,
            string? contractAgency = null,
            decimal? hourlyRate = null);
        Task<Employee?> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee);
        Task<bool> DeleteEmployeeByIdAsync(int id);
    }
}
