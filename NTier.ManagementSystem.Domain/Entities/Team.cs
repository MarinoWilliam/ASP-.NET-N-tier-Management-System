using NTier.ManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }
    }
}
