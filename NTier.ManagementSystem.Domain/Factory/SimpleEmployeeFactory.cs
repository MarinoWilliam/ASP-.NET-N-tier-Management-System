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
            if (source.FirstName != null) target.FirstName = source.FirstName;
            if (source.LastName != null) target.LastName = source.LastName;
            if (source.Email != null) target.Email = source.Email;
            if (source.DepartmentId.HasValue) target.DepartmentId = source.DepartmentId.Value;
            if (source.TeamId.HasValue) target.TeamId = source.TeamId.Value;


            switch (target)
            {
                case FullTimeEmployee t when source is FullTimeEmployee s:
                    if (s.AnnualSalary != 0) t.AnnualSalary = s.AnnualSalary;
                    break;

                case FreelancerEmployee t when source is FreelancerEmployee s:
                    if (s.ContractAgency != null) t.ContractAgency = s.ContractAgency;
                    if (s.HourlyRate != 0) t.HourlyRate = s.HourlyRate;
                    break;

                default:
                    throw new InvalidOperationException("Mismatched or unsupported employee types for update.");
            }

            return target;
        }
    }
}
