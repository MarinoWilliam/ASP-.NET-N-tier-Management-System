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
    public class FreelancerEmployeeConfiguration : IEntityTypeConfiguration<FreelancerEmployee>
    {
        public void Configure(EntityTypeBuilder<FreelancerEmployee> builder) 
        {
            builder.Property(e => e.ContractAgency)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.HourlyRate)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
