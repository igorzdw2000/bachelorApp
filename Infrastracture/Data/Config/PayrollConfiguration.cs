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
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.HasKey(key => key.PayrollId);
            builder.Property(p=>p.PayrollDate).IsRequired();
            builder.Property(p=>p.HourlyRate).IsRequired();
            builder.Property(p=>p.TotalHoursWorked).IsRequired();
            builder.Property(p => p.Earnings).IsRequired();

            builder.HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(key => key.EmployeeId);
                
        }
    }
}
