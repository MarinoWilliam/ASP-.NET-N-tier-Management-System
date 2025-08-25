using NTier.ManagementSystem.Data.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.DTO.EmployeeDTO
{
    public class RetrunFullTimeEmployeeDto : RetrunEmployeeDto
    {
        public decimal AnnualSalary { get; set; }
        public int KPI { get; set; }
    }
}
