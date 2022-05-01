using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Entities.Concrete.TaskStatus;

namespace DataAccess.Configurations
{
    public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.Property(x => x.Status).IsRequired();
            builder.HasData(new TaskStatus { Id = 1, Status = "Ongoing" }, new TaskStatus { Id = 2, Status = "Completed" });
        }
    }
}
