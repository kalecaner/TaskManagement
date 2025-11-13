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
	public class NotificationConfiguration:IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.Property(n => n.Description).IsRequired(true).HasMaxLength(500);
			builder.Property(x=>x.AppUserId).IsRequired(true);
			builder.Property(n => n.State).IsRequired(true);


		}
	}
	
}
