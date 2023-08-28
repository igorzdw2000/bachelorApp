using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Data.Config
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.OfferId);
            builder.Property(p=>p.Address).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255).IsRequired();
            builder.Property(p=>p.OfferDate).IsRequired();

            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId);
        }
    }
    public class OfferDetailsConfiguration : IEntityTypeConfiguration<OfferDetail>
    {
        public void Configure(EntityTypeBuilder<OfferDetail> builder)
        {
            builder.HasKey(k => new { k.OfferId, k.OfferDetailId});

            builder.Property(p=>p.EstimatedMaterialCost).IsRequired();
            builder.Property(p=>p.EstimatedLaborCost).IsRequired();

            builder.HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(p => p.ServiceId);
        }
    }
}
