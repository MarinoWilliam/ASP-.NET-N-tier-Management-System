using NTier.ManagementSystem.Data.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.DTO.EmployeeDTO
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EmployeeType Type { get; set; }
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }
        public int? TeamId { get; set; }
        public decimal? AnnualSalary { get; set; }
        public string? ContractAgency { get; set; }
        public decimal? HourlyRate { get; set; }
    }
}
