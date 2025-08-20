using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTier.ManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.ManagementSystem.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            builder.HasMany(t => t.Employees)
                   .WithOne(e => e.Team)
                   .HasForeignKey(e => e.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Leader)
                   .WithMany() // no reverse nav in Employee
                   .HasForeignKey(t => t.LeaderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
