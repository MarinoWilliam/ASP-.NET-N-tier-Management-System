using NTier.ManagementSystem.Domain.Common.Enums;
using NTier.ManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Factory
{
    public class FullTimeEmployeeFactory : IEmployeeFactory
    {
        public EmployeeType FactoryType => EmployeeType.FullTime;
        public Employee CreateEmployee(string firstName, string lastName, string? email, int? departmentId = null, int? teamId = null, decimal? annualSalary = null, string? contractAgency = null, decimal? hourlyRate = null)
        {
            if (annualSalary == null)
                throw new ArgumentException("Annual salary is required for full-time employees.");

            return new FullTimeEmployee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DepartmentId = departmentId,
                TeamId = teamId,
                AnnualSalary = annualSalary.Value
            };
        }

        public Employee UpdateEmployee(Employee target, Employee source)
        {
            if (target is FullTimeEmployee t && source is FullTimeEmployee s)
            {
                t.FirstName = s.FirstName;
                t.LastName = s.LastName;
                t.Email = s.Email;
                t.DepartmentId = s.DepartmentId;
                t.TeamId = s.TeamId;
                t.AnnualSalary = s.AnnualSalary;
            }
            return target;
        }
    }
}
