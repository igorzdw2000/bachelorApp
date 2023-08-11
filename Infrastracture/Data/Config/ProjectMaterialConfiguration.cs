using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Data.Config
{
    public class ProjectMaterialConfiguration : IEntityTypeConfiguration<ProjectMaterial>
    {
        public void Configure(EntityTypeBuilder<ProjectMaterial> builder)
        {
            builder.HasKey(key => new {key.PId, key.MId});

            builder.HasOne(pm => pm.Project)
                .WithMany(p=>p.ProjectMaterials)
                .HasForeignKey(pm => pm.PId);

            builder.HasOne(ms => ms.MaterialSupplier)
                .WithMany(m=>m.ProjectMaterials)
                .HasForeignKey(ms => ms.MId);
        }
    }
}
