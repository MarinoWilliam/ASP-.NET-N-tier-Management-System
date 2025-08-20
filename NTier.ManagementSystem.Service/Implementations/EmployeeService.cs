using Microsoft.EntityFrameworkCore;
using NTier.ManagementSystem.Data;
using NTier.ManagementSystem.Domain.Common.Enums;
using NTier.ManagementSystem.Domain.Entities;
using NTier.ManagementSystem.Domain.Factory;
using NTier.ManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IEnumerable<IEmployeeFactory> _factories;
        public EmployeeService(ManagementDbContext context, IEnumerable<IEmployeeFactory> factories)
        {
            _managementDbContext = context;
            _factories = factories;
        }

        public async Task<IEnumerable<Employee>> GetAllEmpoyeesAsync()
        {
            return await _managementDbContext.Employees.AsNoTracking().ToListAsync();

        }
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _managementDbContext.Employees.FindAsync(id);

        }
        public async Task<Employee> AddEmployeeAsync
            (
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
            var factory = _factories.FirstOrDefault(f=> f.FactoryType == type);
            if(factory == null) throw new NotSupportedException($"No factory registered for type {type}");
            var employee = factory.CreateEmployee(
                firstName, lastName, email, departmentId, teamId, annualSalary, contractAgency, hourlyRate);
            _managementDbContext.Add(employee);
            await _managementDbContext.SaveChangesAsync();

            return employee;

        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            var currentEmployee = await _managementDbContext.Employees.FindAsync(employee.Id);
            if (currentEmployee == null) return null;
            var factory = _factories.FirstOrDefault(f => f.FactoryType == GetEmployeeType(employee));
            if (factory == null)
                throw new NotSupportedException("Unsupported employee type");

            var updated = factory.UpdateEmployee(currentEmployee, employee);
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
