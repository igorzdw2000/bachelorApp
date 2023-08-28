using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(k => k.ProjectId);
            builder.Property(p=>p.ProjectId).IsRequired();
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p=>p.Description).IsRequired().HasMaxLength(255);

            builder.HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId);

        }
    }
}
