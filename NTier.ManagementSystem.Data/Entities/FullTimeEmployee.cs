using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.Entities
{
    public class FullTimeEmployee : Employee
    {

        public decimal AnnualSalary { get; set; }
        public int? KPI { get; set; }
    }
}
