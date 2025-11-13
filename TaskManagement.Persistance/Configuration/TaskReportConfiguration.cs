using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistance.Configuration
{
	public class TaskReportConfiguration : IEntityTypeConfiguration<TaskReport>
	{
		public void Configure(EntityTypeBuilder<TaskReport> builder)
		{
			builder.Property(tr => tr.Definition).IsRequired(true).HasMaxLength(250);
			builder.Property(tr => tr.Detail).IsRequired(true);
			builder.Property(tr => tr.AppTaskId).IsRequired(true);




		}


	}

}
