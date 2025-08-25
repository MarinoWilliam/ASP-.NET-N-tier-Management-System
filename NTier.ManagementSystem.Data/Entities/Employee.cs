using NTier.ManagementSystem.Data.Common;
using NTier.ManagementSystem.Data.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.Entities
{
    public abstract class Employee : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }

        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
