using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistance.Configuration
{
	public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppRole> builder)
		{
			builder.Property(x => x.Definition)
				.IsRequired(true)
				.HasMaxLength(150);
			builder.HasMany(x => x.Users).WithOne(x => x.Role)
					.HasForeignKey(x => x.AppRoleId);

			
		}
	}
}
