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
    internal class DepartmentService : IDepartmentService
    {
        private readonly ManagementDbContext _managementDbContext;
        private readonly IDepartmentService _departmentService;

        public DepartmentService (ManagementDbContext managementDbContext, IDepartmentService departmentService)
        {
            _managementDbContext = managementDbContext;
            _departmentService = departmentService;
        }
        public Task<Department> AddDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department?> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department?> UpdateDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
