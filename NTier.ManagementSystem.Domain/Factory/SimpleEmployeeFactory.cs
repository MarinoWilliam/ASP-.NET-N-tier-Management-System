using NTier.ManagementSystem.Domain.Common.Enums;
using NTier.ManagementSystem.Domain.Entities;
using System;

namespace NTier.ManagementSystem.Domain.Factory
{
    public static class SimpleEmployeeFactory
    {
        public static Employee CreateEmployee(
            EmployeeType type,
            string firstName,
            string lastName,
            string? email,
            int? departmentId = null,
            int? teamId = null,
            decimal? annualSalary = null,
            string? contractAgency = null,
            decimal? hourlyRate = null)
        {
            switch (type)
            {
                case EmployeeType.FullTime:
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

                case EmployeeType.Freelancer:
                    if (string.IsNullOrEmpty(contractAgency) || hourlyRate == null)
                        throw new ArgumentException("Contract agency and hourly rate are required for freelancers.");

                    return new FreelancerEmployee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        DepartmentId = departmentId,
                        TeamId = teamId,
                        ContractAgency = contractAgency,
                        HourlyRate = hourlyRate.Value
                    };

                default:
                    throw new NotSupportedException($"Employee type '{type}' is not supported.");
            }
        }

        public static Employee UpdateEmployee(Employee target, Employee source)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Email = source.Email;
            target.DepartmentId = source.DepartmentId;
            target.TeamId = source.TeamId;

            switch (target)
            {
                case FullTimeEmployee t when source is FullTimeEmployee s:
                    t.AnnualSalary = s.AnnualSalary;
                    break;

                case FreelancerEmployee t when source is FreelancerEmployee s:
                    t.ContractAgency = s.ContractAgency;
                    t.HourlyRate = s.HourlyRate;
                    break;

                default:
                    throw new InvalidOperationException("Mismatched or unsupported employee types for update.");
            }

            return target;
        }
    }
}
