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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(key => key.EmployeeId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p=>p.Surname).IsRequired();
            builder.Property(p=>p.HourlyRate).IsRequired();
        }
    }
}
