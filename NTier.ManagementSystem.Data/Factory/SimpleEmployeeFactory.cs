using NTier.ManagementSystem.Data.Common.Enums;
using NTier.ManagementSystem.Data.DTO.EmployeeDTO;
using NTier.ManagementSystem.Data.Entities;
using System;

namespace NTier.ManagementSystem.Data.Factory
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
                        EmployeeType = type,
                        DepartmentId = departmentId,
                        TeamId = teamId,
                        AnnualSalary = annualSalary.Value,
                        KPI = 100
                    };

                case EmployeeType.Freelancer:
                    if (string.IsNullOrEmpty(contractAgency) || hourlyRate == null)
                        throw new ArgumentException("Contract agency and hourly rate are required for freelancers.");

                    return new FreelancerEmployee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        EmployeeType = type,
                        DepartmentId = departmentId,
                        TeamId = teamId,
                        ContractAgency = contractAgency,
                        HourlyRate = hourlyRate.Value
                    };

                default:
                    throw new NotSupportedException($"Employee type '{type}' is not supported.");
            }
        }
        public static Employee UpdateEmployee(Employee target, UpdateEmployeeDto dto)
        {
            switch (dto.Type)
            {
                case EmployeeType.FullTime:
                    if (!dto.AnnualSalary.HasValue)
                        throw new ArgumentException("AnnualSalary is required for full-time employees.");
                    break;
                case EmployeeType.Freelancer:
                    if (string.IsNullOrEmpty(dto.ContractAgency) || !dto.HourlyRate.HasValue)
                        throw new ArgumentException("ContractAgency and HourlyRate are required for freelancers.");
                    break;
                default:
                    throw new NotSupportedException($"Employee type '{dto.Type}' is not supported.");
            }

            target.FirstName = dto.FirstName;
            target.LastName = dto.LastName;
            target.Email = dto.Email;
            target.DepartmentId = dto.DepartmentId;
            target.TeamId = dto.TeamId;

            if (target.EmployeeType != dto.Type)
            {
                target.EmployeeType = dto.Type;

                if (dto.Type == EmployeeType.FullTime)
                {
                    if (target is FreelancerEmployee)
                    {
                        // For TPH, we need to ensure the database columns will be set to NULL
                        target.GetType().GetProperty("ContractAgency")?.SetValue(target, null);
                        target.GetType().GetProperty("HourlyRate")?.SetValue(target, null);
                    }

                    // Set FullTime properties
                    target.GetType().GetProperty("AnnualSalary")?.SetValue(target, dto.AnnualSalary);
                }
                else if (dto.Type == EmployeeType.Freelancer)
                {
                    // If changing to Freelancer, ensure FullTime properties are null
                    if (target is FullTimeEmployee)
                    {
                        target.GetType().GetProperty("AnnualSalary")?.SetValue(target, null);
                        target.GetType().GetProperty("KPI")?.SetValue(target, null);
                    }

                    // Set Freelancer properties
                    target.GetType().GetProperty("ContractAgency")?.SetValue(target, dto.ContractAgency);
                    target.GetType().GetProperty("HourlyRate")?.SetValue(target, dto.HourlyRate);
                }
            }
            else
            {
                // Same type, just update the properties
                if (target is FullTimeEmployee fullTime && dto.AnnualSalary.HasValue)
                {
                    fullTime.AnnualSalary = dto.AnnualSalary.Value;
                }
                else if (target is FreelancerEmployee freelancer)
                {
                    if (!string.IsNullOrEmpty(dto.ContractAgency))
                        freelancer.ContractAgency = dto.ContractAgency;
                    if (dto.HourlyRate.HasValue)
                        freelancer.HourlyRate = dto.HourlyRate.Value;
                }
            }

            return target;
        }
    }
}