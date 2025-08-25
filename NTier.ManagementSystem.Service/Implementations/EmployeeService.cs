using Microsoft.EntityFrameworkCore;
using NTier.ManagementSystem.Data;
using NTier.ManagementSystem.Data.Common.Enums;
using NTier.ManagementSystem.Data.DTO.EmployeeDTO;
using NTier.ManagementSystem.Data.Entities;
using NTier.ManagementSystem.Data.Factory;
using NTier.ManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ManagementDbContext _managementDbContext;

        public EmployeeService(ManagementDbContext context)
        {
            _managementDbContext = context;
        }

        public async Task<IEnumerable<RetrunEmployeeDto>> GetAllEmpoyeesAsync()
        {
            return await _managementDbContext.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Team)
                .Select(e => new RetrunEmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Type = e.EmployeeType,
                    Email = e.Email,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : null,
                    TeamId = e.TeamId,
                    TeamName = e.Team != null ? e.Team.Name : null,
                })
                .ToListAsync();
        }

        public async Task<RetrunEmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            // First, get the type to decide which subclass to query
            var baseEmployee = await _managementDbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (baseEmployee == null) return null;

            if (baseEmployee.EmployeeType == EmployeeType.FullTime)
            {
                var employee = await _managementDbContext.Employees
                    .OfType<FullTimeEmployee>()
                      .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Team)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null) return null;

                return new RetrunFullTimeEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Type = employee.EmployeeType,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department?.Name,
                    TeamId = employee.TeamId,
                    TeamName = employee.Team?.Name,
                    AnnualSalary = employee.AnnualSalary,
                    KPI = employee.KPI ?? 0
                };
            }
            else
            {
                var employee = await _managementDbContext.Employees
                    .OfType<FreelancerEmployee>()
                      .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Team)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null) return null;

                return new RetrunFreeLancerEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Type = employee.EmployeeType,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department?.Name,
                    TeamId = employee.TeamId,
                    TeamName = employee.Team?.Name,
                    ContractAgency = employee.ContractAgency,
                    HourlyRate = employee.HourlyRate
                };
            }
        }


        public async Task<Employee> AddEmployeeAsync(
            string firstName,
            string lastName,
            EmployeeType type,
            string? email = null,
            int? departmentId = null,
            int? teamId = null,
            decimal? annualSalary = null,
            string? contractAgency = null,
            decimal? hourlyRate = null)
        {
            var employee = SimpleEmployeeFactory.CreateEmployee(
                type,
                firstName,
                lastName,
                email,
                departmentId,
                teamId,
                annualSalary,
                contractAgency,
                hourlyRate);

            _managementDbContext.Employees.Add(employee);
            await _managementDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            var currentEmployee = await _managementDbContext.Employees.FindAsync(id);
            if (currentEmployee == null) return null;

            var updated = SimpleEmployeeFactory.UpdateEmployee(currentEmployee, employeeDto);
            await _managementDbContext.SaveChangesAsync();
            return updated;
        }

        public async Task<bool> DeleteEmployeeByIdAsync(int id)
        {
            var employee = await _managementDbContext.Employees.FindAsync(id);
            if (employee == null) return false;

            _managementDbContext.Employees.Remove(employee);
            await _managementDbContext.SaveChangesAsync();

            return true;
        }

        private EmployeeType GetEmployeeType(Employee employee)
        {
            return employee switch
            {
                FullTimeEmployee => EmployeeType.FullTime,
                FreelancerEmployee => EmployeeType.Freelancer,
                _ => throw new NotSupportedException("Unsupported employee type")
            };
        }
    }
}
