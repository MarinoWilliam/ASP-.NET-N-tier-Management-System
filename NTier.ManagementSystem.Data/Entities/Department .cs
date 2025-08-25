using NTier.ManagementSystem.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();

        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
    }
}
