using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTier.ManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.Configurations
{
    public class FullTimeEmployeeConfiguration : IEntityTypeConfiguration<FullTimeEmployee>
    {
        public void Configure(EntityTypeBuilder<FullTimeEmployee> builder )
        {
            builder.Property(e => e.AnnualSalary)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(e => e.KPI)
                .IsRequired(false);
        }

    }
}
