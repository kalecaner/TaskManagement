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
	public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
	{
		public void Configure(EntityTypeBuilder<Priority> builder)
		{
			builder.Property(p => p.Definition).IsRequired().HasMaxLength(50);
			builder.HasMany(x => x.AppTasks).WithOne(x => x.Priority).HasForeignKey(x => x.PriorityId);

		}
	}
}
