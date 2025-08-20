using NTier.ManagementSystem.Domain.Common;
using NTier.ManagementSystem.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
