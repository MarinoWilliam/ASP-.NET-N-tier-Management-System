using Microsoft.EntityFrameworkCore;
using NTier.ManagementSystem.Data;
using NTier.ManagementSystem.Domain.Entities;
using NTier.ManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ManagementDbContext _managementDbContext;

        public DepartmentService (ManagementDbContext managementDbContext)
        {
            _managementDbContext = managementDbContext;
        }
        public async Task<Department> AddDepartmentAsync(Department department)
        {
            _managementDbContext.Departments.Add(department);
            await _managementDbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteDepartmentByIdAsync(int id)
        {
            var department = await _managementDbContext.Departments.FindAsync(id);
            if (department == null) return false;

            _managementDbContext.Departments.Remove(department);
            await _managementDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _managementDbContext.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _managementDbContext.Departments.FindAsync(id);
        }

        public async Task<Department?> UpdateDepartmentAsync(Department department)
        {
            var existing = await _managementDbContext.Departments.FindAsync(department.Id);
            if (existing == null) return null;

            existing.Name = department.Name;
            existing.Description = department.Description;

            await _managementDbContext.SaveChangesAsync();
            return existing;
        }
    }
}
