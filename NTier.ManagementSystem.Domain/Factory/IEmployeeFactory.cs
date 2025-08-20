using NTier.ManagementSystem.Domain.Common.Enums;
using NTier.ManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Domain.Factory
{
    public interface IEmployeeFactory
    {
        EmployeeType FactoryType { get; }
        Employee CreateEmployee(
            string firstName,
            string lastName,
            string? email,
            int? departmentId = null,
            int? teamId = null,
            decimal? annualSalary = null,
            string? contractAgency = null,  
            decimal? hourlyRate = null);

        Employee UpdateEmployee(Employee target, Employee source);

    }
}
