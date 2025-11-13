using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistance.Configuration
{
	public class AppTaskConfiguration : IEntityTypeConfiguration<AppTask>
	{
		public void Configure(EntityTypeBuilder<AppTask> builder)
		{
			builder.Property(builder => builder.PriorityId).IsRequired(true);
			builder.Property(builder => builder.AppUserId).IsRequired(false);
			builder.Property(builder => builder.Description).IsRequired(true);
			builder.Property(builder => builder.Title).IsRequired(true).HasMaxLength(250);
			builder.Property(builder => builder.State).IsRequired(true).HasDefaultValue(false);
			builder.HasMany(x => x.TaskReports).WithOne(x => x.AppTask).HasForeignKey(x => x.AppTaskId);






		}
	}
}
