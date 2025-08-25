using NTier.ManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<Department> AddDepartmentAsync(Department department);
        Task<Department?> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentByIdAsync(int id);
    }
}
