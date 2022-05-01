using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CommentDate).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.TaskId).IsRequired();
        }
    }
}
