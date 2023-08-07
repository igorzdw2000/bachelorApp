using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;
using Task = Core.Entities.Task;

namespace Infrastracture.Data.Config
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.TaskId);
            builder.Property(p=>p.TaskName).IsRequired();
            builder.Property(p=>p.TaskDescription).IsRequired().HasMaxLength(255);
            builder.Property(p=>p.StartDate).IsRequired();

            builder.HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(k=>k.ProjectId);

        }
    }
}
