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
                    dto.ContractAgency = null;
                    dto.HourlyRate = null;
                    break;

                case EmployeeType.Freelancer:
                    if (string.IsNullOrEmpty(dto.ContractAgency) || !dto.HourlyRate.HasValue)
                        throw new ArgumentException("ContractAgency and HourlyRate are required for freelancers.");
                    dto.AnnualSalary = null;
                    break;

                default:
                    throw new NotSupportedException($"Employee type '{dto.Type}' is not supported.");
            }

            target.FirstName = dto.FirstName;
            target.LastName = dto.LastName;
            target.Email = dto.Email;
            target.DepartmentId = dto.DepartmentId;
            target.TeamId = dto.TeamId;

            if (target.EmployeeType == dto.Type)
            {
                switch (target)
                {
                    case FullTimeEmployee fullTime:
                        fullTime.AnnualSalary = dto.AnnualSalary!.Value;
                        break;

                    case FreelancerEmployee freelancer:
                        freelancer.ContractAgency = dto.ContractAgency!;
                        freelancer.HourlyRate = dto.HourlyRate!.Value;
                        break;
                }

                return target;
            }

            target.EmployeeType = dto.Type;

            if (dto.Type == EmployeeType.FullTime)
            {
                var fullTime = target as FullTimeEmployee
                               ?? new FullTimeEmployee { Id = target.Id };
                fullTime.FirstName = dto.FirstName;
                fullTime.LastName = dto.LastName;
                fullTime.Email = dto.Email;
                fullTime.DepartmentId = dto.DepartmentId;
                fullTime.TeamId = dto.TeamId;
                fullTime.EmployeeType = EmployeeType.FullTime;
                fullTime.AnnualSalary = dto.AnnualSalary!.Value;
                fullTime.KPI = 100;
                return fullTime;
            }
            else
            {
                var freelancer = target as FreelancerEmployee
                                 ?? new FreelancerEmployee { Id = target.Id };
                freelancer.FirstName = dto.FirstName;
                freelancer.LastName = dto.LastName;
                freelancer.Email = dto.Email;
                freelancer.DepartmentId = dto.DepartmentId;
                freelancer.TeamId = dto.TeamId;
                freelancer.EmployeeType = EmployeeType.Freelancer;
                freelancer.ContractAgency = dto.ContractAgency!;
                freelancer.HourlyRate = dto.HourlyRate!.Value;
                return freelancer;
            }
        }

    }
}