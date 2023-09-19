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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(key => key.InvoiceId);
            builder.Property(p=>p.InvoiceNumber).IsRequired();
            builder.Property(p=>p.InvoiceType).IsRequired();
            builder.Property(p=>p.PaymentStatus).IsRequired();
           

            builder.HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(p => p.ProjectId);
            
        }
    }
    public class InvoiceDetailsConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(key => new { key.InvoiceId, key.InvoiceDetailId });

            builder.HasOne(p => p.Invoice)
                .WithMany()
                .HasForeignKey(i => i.InvoiceId);
        }
    }
}
