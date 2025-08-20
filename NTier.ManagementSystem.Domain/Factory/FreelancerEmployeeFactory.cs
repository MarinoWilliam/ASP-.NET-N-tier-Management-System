using NTier.ManagementSystem.Domain.Common.Enums;
using NTier.ManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Factory
{
    public class FreelancerEmployeeFactory : IEmployeeFactory
    {
        public EmployeeType FactoryType => EmployeeType.Freelancer;

        public Employee CreateEmployee(string firstName, string lastName, string? email, int? departmentId = null, int? teamId = null, decimal? annualSalary = null, string? contractAgency = null, decimal? hourlyRate = null)
        {
            if (contractAgency == null || hourlyRate == null)
                throw new ArgumentException("Freelancers require an agency and hourly rate.");

            return new FreelancerEmployee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                ContractAgency = contractAgency,
                HourlyRate = hourlyRate.Value
            };
        }

        public Employee UpdateEmployee(Employee target, Employee source)
        {
            if (target is FreelancerEmployee t && source is FreelancerEmployee s)
            {
                t.FirstName = s.FirstName;
                t.LastName = s.LastName;
                t.Email = s.Email;
                t.ContractAgency = s.ContractAgency;
                t.HourlyRate = s.HourlyRate;
            }
            return target;
        }
    }
}
