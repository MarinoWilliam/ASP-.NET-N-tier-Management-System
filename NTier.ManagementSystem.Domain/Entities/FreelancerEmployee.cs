using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Entities
{
    public class FreelancerEmployee : Employee
    {
        public string ContractAgency { get; set; } = null!;
        public decimal HourlyRate { get; set; }
    }
}
